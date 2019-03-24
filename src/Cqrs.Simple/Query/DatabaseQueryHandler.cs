using System;
using System.IO;
using System.Linq;

namespace Cqrs.Simple
{
    public abstract class DatabaseQueryHandler<TArguments, TResult> : IHandleQuery<TArguments, TResult>
        where TArguments : IQuery
    {
        protected readonly string Script;
        protected readonly ISession Session;

        protected DatabaseQueryHandler(ISession session)
        {
            Session = session;
            Script = GetType().GetScript();
        }

        public abstract TResult Handle(TArguments message);
    }

    public static class DbHelper
    {
        public static string GetScript(this Type type)
        {
            var name = type.Name
                .Replace("Handler", string.Empty);

            string embeddedName = type.Assembly.GetManifestResourceNames().Where(x =>
            {
                if (x.EndsWith(".sql"))
                {
                    var arr = x.Split('.');
                    if (name.Equals(arr[arr.Length - 2]))
                        return true;
                }

                return false;
            }).FirstOrDefault();

            if(string.IsNullOrEmpty(embeddedName)) throw new FileLoadException("Script can not be loaded.");

            using (Stream stream = type.Assembly.GetManifestResourceStream(embeddedName))
            using (StreamReader reader = new StreamReader(stream ?? throw new FileNotFoundException(name)))
            {
                return reader.ReadToEnd();
            }
        }
    }
}