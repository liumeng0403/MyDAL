using System.Collections.Generic;

namespace MyDAL.Tools
{
    public static class CollectionEx
    {

        public static bool IsEmpty<T>(this List<T> list)
        {
            if (null == list
                || 0 == list.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static bool IsNotEmpty<T>(this List<T> list)
        {
            return !list.IsEmpty();
        }


    }
}
