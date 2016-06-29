namespace Daisy.Core.Config
{
    public class ConfigResult<T>
    {
        public ConfigResult()
        {
            
        }

        public ConfigResult<T> HaveKey(T value)
        {
            this.Value = value;
            this.KeyExist = true;
            return this;
        }

        public ConfigResult<T> NoKey()
        {
            this.Value = default(T);
            this.KeyExist = false;
            return this;
        }

        public T Value { get;private set; }

        public bool KeyExist { get; private set; }
    }
}