using InterviewQuestions.Builder;
using InterviewQuestions.DataModel;

namespace InterviewQuestions
{
    /*  
     *  The genetic code is a set of rules by which DNA or mRNA is translated into proteins (amino acid sequences).
     *  
     *  1) Three nucleotides (or tri-nucleotide), called codons, specify which amino acid will be used.
     *  2) Since codons are defined by three nucleotides, every sequence can be read in three reading frames, depending on the starting point.
     *     The actual reading frame used for translation is determined by a start codon. 
     *     In our case, we will define the start codon to be the most commonly used ATG (in some organisms there may be other start codons).
     *  3) Translation begins with the start codon, which is translated as Methionine (abbreviated as 'M').
     *  4) Translation continues until a stop codon is encountered. There are three stop codons (TAG, TGA, TAA)
     *  
     * 
     *  Included in this project is a comma seperated value (CSV) text file with the codon translations.
     *  Each line of the file has the codon, followed by a space, then the amino acid (or start or stop)
     *  For example, the first line:
     *  CTA,L
     *  should be interpreted as: "the codon CTA is translated to the amino acid L"
     *  
     *  
     *  You should not assume that the input sequence begins with the start codon. Any nucleotides before the start codon should be ignored.
     *  You should not assume that the input sequence ends with the stop codon. Any nucleotides after the stop codon should be ignored.
     * 
     *  For example, if the input DNA sequence is GAACAAATGCATTAATACAAAAA, the output amino acid sequence is MH.
     *  GAACAA ATG CAT TAA TACAAAAA
     *         \ / \ /
     *          M   H
     *          
     *  ATG -> START -> M
     *  CAT -> H
     *  TAA -> STOP
     *  
     */


    public class CodonTranslator
    {
        private TranslationMap _translationMap;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="codonTableFileName">Filename of the DNA codon table.</param>
        public CodonTranslator(string codonTableFileName)
        {
            _translationMap = new CsvCodonDataBuilder().Build(codonTableFileName);
        }

        /// <summary>
        /// Translates a sequence of DNA into a sequence of amino acids.
        /// </summary>
        /// <param name="dna">DNA sequence to be translated.</param>
        /// <returns>Amino acid sequence</returns>
        public string Translate(string dna)
        {
            var result = "";

            dna = dna.Substring(dna.IndexOf("ATG"));

            for (int i = 0; i < dna.Length; i += 3)
            {
                var codon = dna.Substring(i, 3).ToUpper();

                switch (codon)
                {
                    case "GCT":
                    case "GCC":
                    case "GCA":
                    case "GCG":
                        result += "A";
                        break;
                    case "CGT":
                    case "CGC":
                    case "CGA":
                    case "CGG":
                    case "AGA":
                    case "AGG":
                        result += "R";
                        break;
                    case "AAT":
                    case "AAC":
                        result += "N";
                        break;
                    case "GAT":
                    case "GAC":
                        result += "D";
                        break;
                    case "TGT":
                    case "TGC":
                        result += "C";
                        break;
                    case "CAA":
                    case "CAG":
                        result += "Q";
                        break;
                    case "GAA":
                    case "GAG":
                        result += "E";
                        break;
                    case "GGT":
                    case "GGC":
                    case "GGA":
                    case "GGG":
                        result += "G";
                        break;
                    case "CAT":
                    case "CAC":
                        result += "H";
                        break;
                    case "ATT":
                    case "ATC":
                    case "ATA":
                        result += "I";
                        break;
                    case "TTA":
                    case "TTG":
                    case "CTT":
                    case "CTC":
                    case "CTA":
                    case "CTG":
                        result += "L";
                        break;
                    case "AAA":
                    case "AAG":
                        result += "K";
                        break;
                    case "ATG":
                        result += "M";
                        break;
                    case "TTT":
                    case "TTC":
                        result += "F";
                        break;
                    case "CCT":
                    case "CCC":
                    case "CCA":
                    case "CCG":
                        result += "P";
                        break;
                    case "TCT":
                    case "TCC":
                    case "TCA":
                    case "TCG":
                    case "AGT":
                    case "AGC":
                        result += "S";
                        break;
                    case "ACT":
                    case "ACC":
                    case "ACA":
                    case "ACG":
                        result += "T";
                        break;
                    case "TGG":
                        result += "W";
                        break;
                    case "TAT":
                    case "TAC":
                        result += "Y";
                        break;
                    case "GTT":
                    case "GTC":
                    case "GTA":
                    case "GTG":
                        result += "V";
                        break;
                    case "TAA":
                    case "TGA":
                    case "TAG":
                        return result;
                    default:
                        continue;
                }
            }

            return result;
        }
    }
}