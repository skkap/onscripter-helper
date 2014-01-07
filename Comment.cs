using System;
using System.Collections.Generic;
using System.Linq;

namespace onscripter_helper
{
    public class Comment
    {
        public Comment(String comment)
        {
            OriginalText = comment;

            var trimChars = new[] { '\\', '/', '@' };
            comment = comment.TrimStart(trimChars);
            comment = comment.TrimEnd(trimChars);

            if (comment.StartsWith("＜") || comment.StartsWith("■") || comment.StartsWith("▲") || comment.StartsWith("★")
                || comment.StartsWith(";★") || comment.StartsWith("wait ")
                || comment.StartsWith("se1v ") || comment.StartsWith("se2v ") || comment.StartsWith("se3v ")
                || comment.StartsWith("ggg") || comment.StartsWith("locate ") || comment.StartsWith("quakey ")
                || comment.StartsWith("ld ") || comment.StartsWith("gggg") || comment.StartsWith("lol ")
                || comment.StartsWith("se1 ") || comment.StartsWith(";!sd@\n") || comment.StartsWith("bg ")
                || comment.StartsWith("se2 ") || comment.StartsWith("se3 ") || comment.StartsWith(";ld ")
                || comment.StartsWith("wait ") || comment.StartsWith("dll") || comment.StartsWith("bgcopy")
                || comment.StartsWith("autoclick ") || comment.StartsWith("monocro") || comment.StartsWith("mendef ")
                || comment.StartsWith("mend") || comment.StartsWith("video")
                || comment.StartsWith("for ") || comment.StartsWith("quakex ") || comment.StartsWith("lsp2 ") 
                || comment.StartsWith("dllefe ") || comment.StartsWith("mcl ") || comment.StartsWith("mcbg ")
                || comment.StartsWith("cl ") || comment.StartsWith("print ") || comment.StartsWith("csp2 ")
                || comment.StartsWith("bfly1 ") || comment.StartsWith("quiz ") || comment.StartsWith("konoyaro")
                || comment.StartsWith(" msp ") || comment.StartsWith("!d500\n") || comment.StartsWith("next ")
                || comment.StartsWith("mcbg ") || comment.StartsWith("mbg ") || comment.StartsWith("textoff")
                || comment.StartsWith("mpegplay ") || comment.StartsWith("Especially") 
                || comment.StartsWith("`Shannon ") || comment.StartsWith("^don't") || comment.StartsWith("gggg ")
                || comment.StartsWith("　■") || comment.StartsWith("not ") || comment.StartsWith("mendef ")
                || comment.StartsWith("mono ") || comment.StartsWith("mbg ") || comment.StartsWith("mcbg ")
                || comment.StartsWith("sugoroku") || comment.StartsWith("mldg ") || comment.StartsWith("ld_p ") 
                || comment.StartsWith("Beato ") || comment.StartsWith("punctuation")
                || comment.StartsWith("small ") || comment.StartsWith("sorry,") || comment.StartsWith("here is ")
                || comment.StartsWith("in fact,") || comment.StartsWith("new line ")
                || comment.StartsWith(" I think the") || comment.StartsWith("gggg")
                || comment.StartsWith("Asne and Belne?") || comment.StartsWith("would've said ")
                || comment.StartsWith("textoff ") || comment.StartsWith("give ") || comment.StartsWith("bg ")
                || comment.StartsWith("seishuntte") || comment.StartsWith("b")
                || comment.StartsWith("mld ") || comment.StartsWith("mov ") || comment.StartsWith("mldz ") 
                || comment.StartsWith("cbfly ") || comment.StartsWith("vsp ") || comment.StartsWith("lsp ")
                || comment.StartsWith("sub ") || comment.StartsWith("resettimer") || comment.StartsWith("nega ")
                || comment.StartsWith("get ") || comment.StartsWith("me1 ") || comment.StartsWith("close ")
                || comment.StartsWith("change ") || comment.StartsWith("'I'") || comment.StartsWith("/2") 
                || comment.StartsWith(" IT WAS PAIN") || comment.StartsWith("E_MA")
                || comment.StartsWith("$Free3") || comment.StartsWith("	mov") || comment.StartsWith("	bg")
                || comment.StartsWith(" print") || comment.StartsWith("good, good")
                || comment.StartsWith("dont do") || comment.StartsWith("csp ") || comment.StartsWith("woohoo")

                || comment.Equals("“") || comment.Equals(".") || comment.Equals(";")

                || String.IsNullOrWhiteSpace(comment))
            {
                JpPhrases = new List<String>();
                return;
            }
                
            if (comment.StartsWith("!sd@"))
                comment = comment.Substring(4);

            JpPhrases = comment.Split('@').ToList();
        }

        public String OriginalText { get; set; }

        public List<String> JpPhrases { get; set; }

    }
}
