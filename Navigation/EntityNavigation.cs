using System;


namespace RevStack.Mvc
{
    public class EntityNavigation<TEntity>
        where TEntity : class
    {
        public TEntity NextItem { get; set; }
        public TEntity PrevItem { get; set; }
        public TEntity Item { get; set; }
        public string NextPageLink { get; set; }
        public string PrevPageLink { get; set; }
        public string UrlQueryString { get; set; }
        public int Count { get; set; }

        public EntityNavigation()
        {
            NextItem = default(TEntity);
            PrevItem = default(TEntity);
            Item = default(TEntity);
            NextPageLink = null;
            PrevPageLink = null;
            UrlQueryString = null;
            Count = 0;
        }
    }

    public class EntityNavigation
    {
        public string NextPageLink { get; set; }
        public string PrevPageLink { get; set; }
        public string UrlQueryString { get; set; }
        public int Count { get; set; }

        public EntityNavigation()
        {
            NextPageLink = null;
            PrevPageLink = null;
            UrlQueryString = null;
            Count = 0;
        }
    }
}
