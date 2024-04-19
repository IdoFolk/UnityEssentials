namespace IdoFolk_Essentials.Tools.Random
{
    public interface IRandomProvider<T>
    {
        T Provide();
    }
}