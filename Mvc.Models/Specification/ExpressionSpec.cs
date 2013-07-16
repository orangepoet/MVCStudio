//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Linq.Expressions;

//namespace Mvc.Models.Specification {
//    //sunny
//    internal class ExpressionSpec<TEntity> :Specification<TEntity> where TEntity : class {
//        private Expression<Func<object,bool>> exp;
//        public ExpressionSpec(Expression<Func<object,bool>> expression){
//        this.exp= expression;
//    }
//        public override Expression<Func<object, bool>> Expression {
//            get { return this.exp; }
//        }

//    }
//}