using System.ComponentModel;


namespace SmartCardDesc.Db
{
    public abstract class CommonList<T> : DbModel
    {
        public virtual SortableBindingList<T> List { get; set; }
        
        public CommonList()
        {
            List = new SortableBindingList<T>();
        }

        public CommonList(CommonList<T> list)
        {
            List = new SortableBindingList<T>(list.List);
        } 
    }
}
