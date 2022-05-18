using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightIslands
{
    public class DataObject
{
	public int parametersListLength;
	public int playerId;
	public int commandId;
	public List<int> parametersLength;
	public List<string> parameters;
}

    public static class ParserReader
    {
        public static List<byte> tmpArray;

        public static DataObject Decompress(byte[] arr)
        {
            int i = 0;
            DataObject po = new DataObject();
            po.parametersListLength = BitConverter.ToInt32(arr.SubArray(0, 4), 0);
            po.playerId = BitConverter.ToInt32(arr.SubArray(4, 4), 0);
            po.commandId = BitConverter.ToInt32(arr.SubArray(8, 4), 0);
            po.parametersLength = new List<int>();
            int from = 0;
            for (i = 0; i < po.parametersListLength; i++)
            {
                from = 12 + i * 4;
                byte[] tmp = arr.SubArray(from, 4);
                po.parametersLength.Add(BitConverter.ToInt32(tmp, 0));
            }
            int paramsBegin = from + 4;
            po.parameters = new List<string>();
            List<byte> bArr = new List<byte>();
            for (int l = 0; l < po.parametersLength.Count; l++)
            {
                int offset = 0;
                if (l > 0) for (int p = 0; p < l; p++) offset += po.parametersLength[p];
                bArr.Clear();
                for (i = paramsBegin + offset; i < paramsBegin + offset + po.parametersLength[l]; i++)
                {
                    bArr.Add(arr[i]);
                }
                po.parameters.Add(System.Text.Encoding.UTF8.GetString(bArr.ToArray()));
            }
            return po;
        }
    }

    public static class ParserCreator
    {
        public static byte[] resultArray;
        public static List<byte> tmpArray;

        public static byte[] Compress(int pId, int cId, List<string> parameters)
        {
            List<byte[]> paramsList = new List<byte[]>();
            List<byte[]> paramsListLength = new List<byte[]>();
            for (int i = 0; i < parameters.Count; i++)
            {
                paramsList.Add(Encoding.UTF8.GetBytes(parameters[i]));
            }
            for (int i = 0; i < paramsList.Count; i++)
            {
                paramsListLength.Add(BitConverter.GetBytes(paramsList[i].Length));
            }
            tmpArray = new List<byte>();
            tmpArray.AddRange(BitConverter.GetBytes(paramsListLength.Count));
            tmpArray.AddRange(BitConverter.GetBytes(pId));
            tmpArray.AddRange(BitConverter.GetBytes(cId));
            for (int i = 0; i < paramsListLength.Count; i++)
            {
                tmpArray.AddRange(paramsListLength[i]);
            }
            for (int i = 0; i < paramsList.Count; i++)
            {
                tmpArray.AddRange(paramsList[i]);
            }
            resultArray = tmpArray.ToArray();
            return resultArray;
        }
    }

}
