namespace MyAPITemplate.Resources
{
    public abstract class Resource<T>
    {
        public abstract Dictionary<string, object>? Make(T _obj);
        public abstract T Generate();

        public Dictionary<string, object> Make(ICollection<T> _objs) {
            return new Dictionary<string, object> {
                { "data" , _objs.Select(Make).Where(o => o != null).ToArray() }
            };
        }

        public List<T> Generate(int _count) {
            List<T> _new = new List<T>(_count);

            for (int i = 0; i < _count; i++) {
                _new.Add(Generate());
            }

            return _new;
        }

    }
}
