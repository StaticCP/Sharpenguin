using System;
using System.Collections.Generic;
namespace Sharpenguin {
    /// <summary>
    /// Provides a utility to load all handlers of a given type.
    /// </summary>
    public static class HandlerLoader {
        /// <summary>
        /// Gets the handlers of the type T.
        /// </summary>
        /// <returns>An array of initiated handlers of the type T.</returns>
        /// <typeparam name="T">The type of handler to load and return.</typeparam>
        public static T[] GetHandlers<T>() {
            List<T> handlers = new List<T>();
            foreach(System.Type type in System.Reflection.Assembly.GetExecutingAssembly().GetTypes()) {
                if(typeof(T).IsAssignableFrom(type)) {
                    try {
                        T handler = (T)(type.GetConstructor(new System.Type[] { })).Invoke(new object[] { });
                        handlers.Add(handler);
                    } catch(System.Exception) {
                        Configuration.Configuration.Logger.Warn("The handler of type '" + type.FullName + "' could not be loaded. Most likely, it does not have a constructor that takes 0 arguments.");
                    }
                }
            }
            return handlers.ToArray();
        }
    }
}

