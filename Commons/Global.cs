using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bingzer.Bison.Commons
{
    public class Global
    {

        public static object[] ToObjectArray(object primitiveArray)
        {
            if (primitiveArray != null && primitiveArray.GetType().IsArray
                    && primitiveArray.GetType().GetElementType().IsPrimitive)
            {
                /*
                if (primitiveArray.GetType().GetElementType().Name.Equals("int"))
                    return (object[]) ToObject((int[])primitiveArray);
                else if (primitiveArray.GetType().GetElementType().Name.Equals("float"))
                    return ToObject((float[])primitiveArray);
                else if (primitiveArray.GetType().GetElementType().Name.Equals("double"))
                    return ToObject((double[])primitiveArray);
                else if (primitiveArray.GetType().GetElementType().Name.Equals("long"))
                    return ToObject((long[])primitiveArray);
                else if (primitiveArray.GetType().GetElementType().Name.Equals("short"))
                    return ToObject((short[])primitiveArray);
                else if (primitiveArray.GetType().GetElementType().Name.Equals("byte"))
                    return ToObject((byte[])primitiveArray);
                else if (primitiveArray.GetType().GetElementType().Name.Equals("char"))
                    return ToObject((char[])primitiveArray);
                else if (primitiveArray.GetType().GetElementType().Name.Equals("bool"))
                    return ToObject((bool[])primitiveArray);
                 */
                return (object[])primitiveArray;
            }

            // otherwise return null
            return null;
        }

        public static object ToObject(object primitive)
        {
            if (primitive != null && primitive.GetType().IsArray)
            {
                if (primitive.GetType().GetElementType().Name.Equals("int"))
                    return ToObject((int)int.Parse("" + primitive));
                else if (primitive.GetType().GetElementType().Name.Equals("float"))
                    return ToObject((float)float.Parse("" + primitive));
                else if (primitive.GetType().GetElementType().Name.Equals("double"))
                    return ToObject((double)double.Parse("" + primitive));
                else if (primitive.GetType().GetElementType().Name.Equals("long"))
                    return ToObject((long)long.Parse("" + primitive));
                else if (primitive.GetType().GetElementType().Name.Equals("short"))
                    return ToObject((short)short.Parse("" + primitive));
                else if (primitive.GetType().GetElementType().Name.Equals("byte"))
                    return ToObject((byte)byte.Parse("" + primitive));
                else if (primitive.GetType().GetElementType().Name.Equals("char"))
                    return ToObject((char)("" + primitive).ToCharArray()[0]);
                else if (primitive.GetType().GetElementType().Name.Equals("boolean"))
                    return ToObject((bool)Boolean.Parse("" + primitive));
            }
            return null;
        }

        public static int ToObject(int primitive)
        {
            return primitive;
        }

        public static float ToObject(float primitive)
        {
            return primitive;
        }

        public static Double ToObject(double primitive)
        {
            return primitive;
        }

        public static long ToObject(long primitive)
        {
            return primitive;
        }

        public static short ToObject(short primitive)
        {
            return primitive;
        }

        public static Byte ToObject(byte primitive)
        {
            return primitive;
        }

        public static char ToObject(char primitive)
        {
            return primitive;
        }

        public static Boolean ToObject(bool primitive)
        {
            return primitive;
        }

        public static float[] ToObject(float[] primitive)
        {
            float[] obj = new float[primitive.Length];
            for (int i = 0; i < primitive.Length; i++)
            {
                obj[i] = primitive[i];
            }
            return obj;
        }

        public static Boolean[] ToObject(bool[] primitive)
        {
            Boolean[] obj = new Boolean[primitive.Length];
            for (int i = 0; i < primitive.Length; i++)
            {
                obj[i] = primitive[i];
            }
            return obj;
        }

        public static int[] ToObject(int[] primitive)
        {
            int[] obj = new int[primitive.Length];
            for (int i = 0; i < primitive.Length; i++)
            {
                obj[i] = primitive[i];
            }
            return obj;
        }

        public static long[] ToObject(long[] primitive)
        {
            long[] obj = new long[primitive.Length];
            for (int i = 0; i < primitive.Length; i++)
            {
                obj[i] = primitive[i];
            }
            return obj;
        }

        public static Double[] ToObject(double[] primitive)
        {
            Double[] obj = new Double[primitive.Length];
            for (int i = 0; i < primitive.Length; i++)
            {
                obj[i] = primitive[i];
            }
            return obj;
        }

        public static short[] ToObject(short[] primitive)
        {
            short[] obj = new short[primitive.Length];
            for (int i = 0; i < primitive.Length; i++)
            {
                obj[i] = primitive[i];
            }
            return obj;
        }

        public static Byte[] ToObject(byte[] primitive)
        {
            Byte[] obj = new Byte[primitive.Length];
            for (int i = 0; i < primitive.Length; i++)
            {
                obj[i] = primitive[i];
            }
            return obj;
        }

        public static char[] ToObject(char[] primitive)
        {
            char[] obj = new char[primitive.Length];
            for (int i = 0; i < primitive.Length; i++)
            {
                obj[i] = primitive[i];
            }
            return obj;
        }
    }
}
