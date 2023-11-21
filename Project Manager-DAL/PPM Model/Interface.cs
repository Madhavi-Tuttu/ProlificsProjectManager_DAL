namespace PPM.Model
{
    public interface Ioperation<T>
    {
        void Add(T obj);
        T ViewAll();
        T GetById(int Id);
        void Delete(int Id);
     }
}