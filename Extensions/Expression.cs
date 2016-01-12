using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RevStack.Mvc
{
    public static partial class Extensions
    {
        /// <summary>
        /// iterates model properties to output a document string body
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string ToDocumentString<T>(this T entity)
        {

            IEnumerable<T> data = new[] { entity };
            var props = typeof(T).GetProperties();
            var sb = Expression.Parameter(typeof(StringBuilder));
            var obj = Expression.Parameter(typeof(T));
            Expression body = sb;
            foreach (var prop in props)
            {
                string propertyName = prop.Name;
                int labelIndex = propertyName.IndexOf("Label");
                int optionIndex = propertyName.IndexOf("Option");
                int answerIndex = propertyName.IndexOf("Answer");
                int headerLabelIndex = propertyName.IndexOf("HeaderLabel");
                bool isSelectListOptions = false;
                bool isEnumerableAnswer = false;
                string type = prop.PropertyType.Name.ToString();
                int typeIndex = type.IndexOf("ICollection");
                int ienumerableStringIndex = type.IndexOf("IEnumerable");
                bool guidIndex = (type.IndexOf("Guid") > -1);

                if (typeIndex > -1)
                {
                    isSelectListOptions = true;
                }
                if (ienumerableStringIndex > -1)
                {
                    isEnumerableAnswer = true;
                }
                if (labelIndex == 0)
                {
                    body = StringBuilderAppend(body, Expression.Property(obj, prop));
                    body = StringBuilderAppend(body, Expression.Constant(Environment.NewLine));
                }
                else if (optionIndex == 0 || answerIndex == 0 || headerLabelIndex == 0)
                {
                    body = StringBuilderAppend(body, Expression.Property(obj, prop));
                    body = StringBuilderAppend(body, Expression.Constant(Environment.NewLine + Environment.NewLine));
                }
                else if (!isSelectListOptions && !isEnumerableAnswer && !guidIndex)
                {
                    propertyName = propertyName.ToPhraseCase();
                    body = StringBuilderAppend(body, Expression.Constant(propertyName));
                    body = StringBuilderAppend(body, Expression.Constant(":"));
                    body = StringBuilderAppend(body, Expression.Constant(Environment.NewLine));
                    try
                    {
                        body = StringBuilderAppend(body, Expression.Property(obj, prop));
                    }
                    catch (Exception)
                    {

                    }

                    body = StringBuilderAppend(body, Expression.Constant(Environment.NewLine + Environment.NewLine));
                }
                else if (isEnumerableAnswer)
                {
                    propertyName = propertyName.ToPhraseCase();
                    body = StringBuilderAppend(body, Expression.Constant(Environment.NewLine));
                    body = StringBuilderAppend(body, Expression.Constant(propertyName));
                    body = StringBuilderAppend(body, Expression.Constant(":"));
                    body = StringBuilderAppend(body, Expression.Constant(Environment.NewLine));
                    var list = (IEnumerable<string>)(prop.GetValue(entity));
                    foreach (var s in list)
                    {
                        body = StringBuilderAppend(body, Expression.Constant(s));
                        body = StringBuilderAppend(body, Expression.Constant(Environment.NewLine));
                    }
                }
            }
            body = Expression.Call(body, "AppendLine", Type.EmptyTypes);
            var lambda = Expression.Lambda<Func<StringBuilder, T, StringBuilder>>(body, sb, obj);
            var func = lambda.Compile();

            var result = new StringBuilder();
            foreach (T row in data)
            {
                func(result, row);
            }
            return result.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        static Expression StringBuilderAppend(Expression instance, Expression arg)
        {
            var method = typeof(StringBuilder).GetMethod("Append", new Type[] { arg.Type });
            return Expression.Call(instance, method, arg);
        }

    }
}
