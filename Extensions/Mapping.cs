using System;
using System.Threading.Tasks;

namespace RevStack.Mvc
{
    public static partial class Extensions
    {
        public static T CopyPropertiesFrom<T>(this T target, object source)
        {
            return copyPropertiesFrom(target, source);
        }

        public static T CopyPropertiesFrom<T>(this T target, object source, bool nullifyDefaultSelectValue)
        {
            if (nullifyDefaultSelectValue)
            {
                return copyPropertiesFromNullifySelect(target, source);
            }
            return copyPropertiesFrom(target, source);
        }

        public static Task<T> CopyPropertiesFromAsync<T>(this T target, object source)
        {
            return Task.FromResult(copyPropertiesFrom(target, source));
        }

        public static Task<T> CopyPropertiesFromAsync<T>(this T target, object source, bool nullifyDefaultSelectValue)
        {
            return Task.FromResult(CopyPropertiesFrom(target, source,nullifyDefaultSelectValue));
        }



        private static T copyPropertiesFrom<T>(T target, object source)
        {
            var targetType = target.GetType();
            var sourceType = source.GetType();

            var sourceProps = sourceType.GetProperties();
            foreach (var propInfo in sourceProps)
            {
                //Get the matching property from the target
                var toProp =
                    (targetType == sourceType) ? propInfo : targetType.GetProperty(propInfo.Name);

                //If it exists and it's writeable
                if (toProp != null && toProp.CanWrite)
                {
                    //Copy non null value from the source to the target
                    var value = propInfo.GetValue(source, null);
                    if (value != null && value.ToString() != "")
                    {
                        toProp.SetValue(target, value, null);
                    }
                }
            }
            return target;
        }

        private static T copyPropertiesFromNullifySelect<T>(T target, object source)
        {
            var targetType = target.GetType();
            var sourceType = source.GetType();

            var sourceProps = sourceType.GetProperties();
            foreach (var propInfo in sourceProps)
            {
                //Get the matching property from the target
                var toProp =
                    (targetType == sourceType) ? propInfo : targetType.GetProperty(propInfo.Name);

                //If it exists and it's writeable
                if (toProp != null && toProp.CanWrite)
                {
                    //Copy the value from the source to the target
                    var value = propInfo.GetValue(source, null);
                    if (value != null && value.ToString().ToLower() == "select")
                    {
                        value = null;
                    }
                    toProp.SetValue(target, value, null);
                }
            }
            return target;
        }
    }
}
