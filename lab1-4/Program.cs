using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_4
{
    class Program
    {

        static void Main(string[] str)
        {
            Console.Write("Hello world!");
            Console.ReadLine();

            byte[] input = File.ReadAllBytes("test.exe");

            string inputString = Encoding.Default.GetString(input);

            //byte [] inputStringBytes =  Encoding.Default.GetBytes(inputString);

            //File.WriteAllBytes("test2.exe", inputStringBytes);

            

            
            HuffmanTree huffmanTree = new HuffmanTree();
            huffmanTree.BuildTree(inputString);

            byte[] encoded = Encoded(inputString, huffmanTree);
            Console.WriteLine("encoded: " + encoded);
            File.WriteAllBytes("test", encoded);

            byte[] inputEncoded = File.ReadAllBytes("test");

            string inputEncodedString = Encoding.Default.GetString(inputEncoded);

            // Decode
            string decoded = decoder(inputEncodedString, huffmanTree);

            File.WriteAllBytes("test1.exe", Encoding.Default.GetBytes(decoded));

            Console.WriteLine("Decoded: " + decoded);

            Console.ReadLine();
        }

        public static byte[] Encoded(string input, HuffmanTree huffmanTree)
        {

            BitArray encoded = huffmanTree.Encode(input); //Encode the tree

            //First show the generated binary output
            string bitText = string.Join(string.Empty, encoded.Cast<bool>().Select(bit => bit ? "1" : "0"));

            //Next, convert the binary output to the new characterized output string.       
            byte[] bytes = new byte[(encoded.Length / 8) + 1];
            encoded.CopyTo(bytes, 0);

            return bytes;
        }

        public static string decoder(string input, HuffmanTree huffmanTree)
        {
            bool[] boolAr = new BitArray(Encoding.Default.GetBytes(input)).Cast<bool>().Take(huffmanTree.BitCountForTree).ToArray();
            BitArray encoded = new BitArray(boolAr);

            string decoded = huffmanTree.Decode(encoded);


            return huffmanTree.Decode(encoded); ;
        }
    }
}
