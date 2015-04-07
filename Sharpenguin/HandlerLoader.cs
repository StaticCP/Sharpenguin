using System;
using System.Collections.Generic;
namespace Sharpenguin {
    public static class HandlerLoader {
        public static T[] GetHandlers<T>() {
            List<T> handlers = new List<T>();
            foreach(System.Type type in System.Reflection.Assembly.GetExecutingAssembly().GetTypes()) {
                if(typeof(T).IsAssignableFrom(type)) {
                    try {
                        T handler = (T) (type.GetConstructor(new System.Type[] {})).Invoke(new object[] {});
                        handlers.Add(handler);
                    }catch(System.Exception ex) {

                    }
                }
            }
            return handlers.ToArray();
        }
    }
}

