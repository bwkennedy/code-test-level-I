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
        //Goal: Return the letter string of the amino acids depending on the codon sequence
        
        public string aminoAcids;
        
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
            bool pastStart = false;
            bool pastStop = false;

            //First, I need to loop through each character in the string in order to search for the amino acid sequences
            for (int iCount = 0; iCount < dna.Length; iCount++)
            {
                //Determine if I am at the start of a new three set character amino acid sequence by using the modulus operator
                if ((iCount + 1) % 3 == 1 && pastStop == false)
                {
                    //If I am at the start of the string, build the codon acording to the current character and the next two characters
                    string codon;
                    string charOne = dna[iCount].ToString();
                    string charTwo = dna[iCount + 1].ToString();
                    string charThree = dna[iCount + 2].ToString();
                    codon = string.Concat(charOne, charTwo, charThree);

                    //Determine if we have passed the start yet

                    if (pastStart == false)
                    {
                        if (codon == "ATG")
                        {
                            //Change the start variable to true and add the first amino acid character to the return value
                            pastStart = true;
                            //Need to understand how to get the amino acid according to my codon string
                            this.aminoAcids = "M";
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        //Determine if I am at a stop codon, if not continue to add to the amino acid string
                        if (codon == "TAG" || codon == "TGA" || codon == "TAA")
                        {
                            //Change the pastStop variable to false, so the program will not search
                            // through any future amino acids
                            pastStop = true;
                        }
                        else
                        {
                            //If past start as well as not past find the correct amino acid and add to the string
                            bool hasValue = _translationMap.CodonMap.TryGetValue(codon, out string newAmino);

                            if (hasValue == true)
                            {
                                this.aminoAcids += newAmino;
                            }
                            else
                            {
                                //Return error message explaining that the system could not find the new amino acid value
                            }
                        }
                    }
                }
            }

            return this.aminoAcids;

            //While I did not complete the project within the assigned time limit, here are some lessons that I learned
            //1. I failed to read through each of the tests inidividually to recognize that there is an off-balanced start which does not fall in line with my modulus approach for building codons.
            //I would need to search the string for the first instance of the ATG, and then use the character following that sequence to start building the rest of the amino acid string
            //2. I also did not take enough time to understand how the translation map works. I was only able to fulfill the first test, and this was mainly due to my inability to understand how to
            //effectively search for a certain value in the CSV and return the value according to the key of the sequence.
            //I would need to develop a way to use a current method in the system in order to find the amino acid I need according to my codon. I would then be able to successfully
            //concatenate the character to the string of the amino acids to pass back.

            //Thank you for your time and appreciation.
        }
        }
    }
}
