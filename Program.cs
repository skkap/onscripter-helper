using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace onscripter_helper
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please pass a path to valid 'nscript.dat' file.");
                return 1;
            }
            var path = args[0];
            if (!File.Exists(path))
            {
                Console.WriteLine("File ont found.");
                return 1;
            }

            var fileText = File.ReadAllText(path, Encoding.GetEncoding(932));
            fileText = fileText.Replace("\r\n", "\n"); // will be converted back before saving

            // extra comments
            fileText = fileText.Replace(";;br", "\n\n");
            fileText = fileText.Replace(";br", "\n\n");

            var blocks = ParseBlocks(fileText);
            Console.WriteLine("{0} blocks with eng phrases.", blocks.Count);
            Console.WriteLine("{0} ENG phrases in blocks.", blocks.Sum(p => p.PhrasesEng.Count));
            Console.WriteLine("{0} JP phrases in blocks.", blocks.Sum(p => p.PhrasesJp.Count));

            #region statistic
            var engSameAsJpBlocks = blocks.Where(b => b.PhrasesEng.Count == b.PhrasesJp.Count).ToList();
            Console.WriteLine("{0} blocks where eng == jp. [{1:N2}%]", engSameAsJpBlocks.Count, (engSameAsJpBlocks.Count * 100) / (double)blocks.Count);

            var engLessThenJpCount = blocks.Count(b => b.PhrasesEng.Count < b.PhrasesJp.Count);
            Console.WriteLine("{0} blocks where eng < jp. [{1:N2}%]", engLessThenJpCount, (engLessThenJpCount * 100) / (double)blocks.Count);

            var engMoreThenJpCount = blocks.Count(b => b.PhrasesEng.Count > b.PhrasesJp.Count);
            Console.WriteLine("{0} blocks where eng > jp. [{1:N2}%]", engMoreThenJpCount, (engMoreThenJpCount * 100) / (double)blocks.Count);

            #endregion

            #region out files


            var incorrectBlocks = blocks.Where(b => b.PhrasesEng.Count > b.PhrasesJp.Count || b.PhrasesEng.Count < b.PhrasesJp.Count);
            var incorrectBlocksSb = new StringBuilder();
            foreach (var block in incorrectBlocks)
            {
                if (block.PhrasesEng.Count > block.PhrasesJp.Count)
                {
                    incorrectBlocksSb.AppendLine("------------------- " + block.PhrasesEng.Count.ToString() + "---" + block.PhrasesJp.Count.ToString() + " -------------------");
                    incorrectBlocksSb.AppendLine("------------------- " + "ENG > JP" + " -------------------");
                    incorrectBlocksSb.AppendLine(block.OriginalText.Replace("\n", "\r\n"));
                    incorrectBlocksSb.AppendLine("-------------------------------------------------------");
                    incorrectBlocksSb.AppendLine();
                }
                if (block.PhrasesEng.Count < block.PhrasesJp.Count)
                {
                    incorrectBlocksSb.AppendLine("------------------- " + block.PhrasesJp.Count.ToString() + "---" + block.PhrasesEng.Count.ToString() + " -------------------");
                    incorrectBlocksSb.AppendLine("------------------- " + "JP > ENG" + " -------------------");
                    incorrectBlocksSb.AppendLine(block.OriginalText.Replace("\n", "\r\n"));
                    incorrectBlocksSb.AppendLine("-------------------------------------------------------");
                    incorrectBlocksSb.AppendLine();
                }

            }
            File.WriteAllText("incorrect-blocks.txt", incorrectBlocksSb.ToString());


            #endregion

            #region result file

            var result = ReplaseEngPhrasesWithJp(fileText, engSameAsJpBlocks);
            result = result.Replace("\n", "\r\n");
            File.WriteAllText(path + ".fixed", result, Encoding.GetEncoding(932));

            #endregion

            Console.WriteLine("Press any key...");
            Console.ReadKey();
            return 0;
        }

        private static String ReplaseEngPhrasesWithJp(String originalFileText, IReadOnlyList<Block> engPhrasesToReplase)
        {
            var currentProgressStep = 1;
            Console.WriteLine("Replacement started for {0} blocks.", engPhrasesToReplase.Count);
            var sb = new StringBuilder(originalFileText);
            var globalLengthDiff = 0;
            for (int i = 0; i < engPhrasesToReplase.Count; i++)
            {
                var block = engPhrasesToReplase[i];

                for (var j = 0; j < block.PhrasesEng.Count; j++)
                {
                    var engPhrase = block.PhrasesEng[j];
                    var jpPhrase = block.PhrasesJp[j];

                    sb.Remove(block.Position + engPhrase.Position + 1 + globalLengthDiff, engPhrase.Length);
                    sb.Insert(block.Position + engPhrase.Position + 1 + globalLengthDiff, jpPhrase);

                    globalLengthDiff += jpPhrase.Length - engPhrase.Length;
                }

                var progress = (i * 100) / engPhrasesToReplase.Count;
                if (progress >= currentProgressStep)
                {
                    Console.WriteLine("{0}% completed.", currentProgressStep);
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    currentProgressStep += 1;
                }
            }
            Console.WriteLine();
            Console.WriteLine("Finished!");
            return sb.ToString();
        }

        private static List<Block> ParseBlocks(String fileText)
        {
            var blockPattern = new Regex(@"((?:.+\n?)+)\n?");

            var blockMatches = blockPattern.Matches(fileText);
            var blocks = new List<Block>();

            foreach (Match blockMatch in blockMatches)
            {
                var block = new Block(blockMatch.Groups[1].ToString(), blockMatch.Index);
                if (block.PhrasesEng.Count > 0)
                    blocks.Add(block);
            }

            return blocks;
        }
    }
}
