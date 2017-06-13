using System.ComponentModel;


namespace SmartCardDesc.Db
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CommonList<T> : DbModel
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual SortableBindingList<T> List { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public CommonList()
        {
            List = new SortableBindingList<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public CommonList(CommonList<T> list)
        {
            List = new SortableBindingList<T>(list.List);
        } 
    }
}
