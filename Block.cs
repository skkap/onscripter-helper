using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace onscripter_helper
{
    public class Block
    {
        public Block(String blockString, Int32 position)
        {
            this.OriginalText = blockString;
            this.Position = position;

            var engPhrasePattern = new Regex(@"`([^`]*)`[\\,@,/]");
            var commentPattern = new Regex(@"^;(.*)$", RegexOptions.Multiline);
            var engPhraseMatches = engPhrasePattern.Matches(blockString);

            PhrasesEng = new List<Phrase>();
            foreach (Match engPhraseMatch in engPhraseMatches)
            {
                PhrasesEng.Add(new Phrase
                {
                    Position = engPhraseMatch.Index,
                    Text = engPhraseMatch.Groups[1].ToString()
                });
            }

            if (PhrasesEng.Count > 0)
            {
                var commentMatches = commentPattern.Matches(blockString);
                var commentStrings = (from object commentMatch in commentMatches select ((Match)commentMatch).Groups[1].ToString()).ToList();
                Comments = new List<Comment>();
                commentStrings.ForEach(c => Comments.Add(new Comment(c)));
            }
        }

        public Int32 Position { get; set; }

        public String OriginalText { get; set; }

        public List<Phrase> PhrasesEng { get; set; }

        public List<Comment> Comments { get; set; }

        public List<String> PhrasesJp
        {
            get
            {
                var phrasesJp = new List<String>();
                Comments.ForEach(c => phrasesJp.AddRange(c.JpPhrases));
                return phrasesJp;
            }
        }
    }
}
