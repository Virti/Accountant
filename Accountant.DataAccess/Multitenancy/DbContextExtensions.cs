using System;
using System.Linq.Expressions;
using System.Reflection;
using Accountant.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Accountant.DataAccess.Multitenancy
{
    public abstract class BaseMultitenantContext<TContext> : DbContext
        where TContext : DbContext
    {
        public Guid TenantId { get; private set; }

        private MethodInfo ConfigureGlobalFiltersMethodInfo = typeof(TContext).GetMethod(nameof(ConfigureGlobalFilters), BindingFlags.Instance | BindingFlags.NonPublic);

        protected BaseMultitenantContext(DbContextOptions<TContext> options)
            : base (options)
        {
        }

        public void SetTenantId(Guid tenantId)
            => TenantId = tenantId;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                

                ConfigureGlobalFiltersMethodInfo
                    .MakeGenericMethod(entityType.ClrType)
                    .Invoke(this, new object[] { modelBuilder, entityType });
            }
        }

        protected virtual void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType)
            where TEntity : class, IBaseEntity
        {
            if (entityType.BaseType == null && ShouldFilterEntity<TEntity>())
            {
                var filterExpression = CreateFilterExpression<TEntity>();
                if (filterExpression != null)
                {
                    modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
                }
            }
        }
        protected virtual Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
            where TEntity : class, IBaseEntity
        {
            Expression<Func<TEntity, bool>> expression = null;

            if(typeof(IBaseTenantEntity).IsAssignableFrom(typeof(TEntity)))
            {

                Expression<Func<TEntity, bool>> mustHaveTenantFilter = e => ((IBaseTenantEntity)e).TenantId == TenantId;
                expression = expression == null ? mustHaveTenantFilter : CombineExpressions(expression, mustHaveTenantFilter);
            }

            return expression;
        }

        protected virtual bool ShouldFilterEntity<TEntity>()
            where TEntity : class, IBaseEntity
        {
            return typeof(IBaseTenantEntity).IsAssignableFrom(typeof(TEntity));
        }

        protected virtual Expression<Func<T, bool>> CombineExpressions<T>(Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expression1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expression1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expression2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expression2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
        }
        
        class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                if (node == _oldValue)
                {
                    return _newValue;
                }

                return base.Visit(node);
            }
        }
    }
}