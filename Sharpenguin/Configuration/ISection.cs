using System.Linq;

namespace Sharpenguin {
    public interface ISection<T> where T : IItem {
        T this[int id] {
            get;
        }

        T Where(System.Func<T, bool> predictator);
    }
}

