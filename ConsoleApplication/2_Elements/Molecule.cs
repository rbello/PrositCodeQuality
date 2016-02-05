using System;
using System.Collections.Generic;

namespace ConsoleApplication.Elements
{
    public class Molecule : AElement
    {
        public Atom[] Atoms { get; private set; }

        public Molecule(Atom[] atoms) : base(null, null, -1)
        {
            if (atoms == null || atoms.Length < 2)
            {
                throw new ArgumentException("At least 2 atomes in a molecule");
            }
            Name = Symbol = GetChemicalFormula(atoms);
            Mass = SumMass(atoms);
            Atoms = atoms;
        }

        public static double SumMass(Atom[] atoms)
        {
            double sum = 0;
            foreach (Atom a in atoms)
            {
                sum += a.Mass;
            }
            return sum;
        }

        public static string GetChemicalFormula(Atom[] atoms)
        {
            var dico = new Dictionary<string, int>();
            foreach (Atom atom in atoms)
            {
                if (dico.ContainsKey(atom.Symbol))
                    dico[atom.Symbol]++;
                else
                    dico[atom.Symbol] = 1;
            }
            var formula = "";
            foreach (string symbol in dico.Keys)
            {
                formula += symbol;
                int count = dico[symbol];
                if (count > 1) formula += count;
            }
            return formula;
        }

    }
}
