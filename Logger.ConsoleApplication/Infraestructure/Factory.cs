namespace Logger.ConsoleApplication.Infraestructure
{
    public static class Factory
    {
        public static T GetInstance<T>() where T : new()
        {
            return new T();
        }
    }
}
