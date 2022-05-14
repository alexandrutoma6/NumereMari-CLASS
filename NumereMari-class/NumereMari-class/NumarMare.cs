using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumereMari_class
{
    public class NumarMare
    {
        private int[] v = new int[100];
        private int lenght;
        private char sign;
        public NumarMare(string line = "0") //constructorul
        {
            int j = line.Length - 1;
            Array.Resize(ref v, line.Length - 1);//marimea lui v se modifica in functie de lungimea numarului introdus
            this.sign = line[0];
            for (int i = 0; i < line.Length - 1; i++)
            {
                this.v[i] = int.Parse(line[j].ToString());
                j--;
            }
            this.lenght = line.Length - 1;
        }
        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append(sign.ToString());
            for (int i = v.Length - 1; i >= 0; i--)
            {
                s.Append(v[i].ToString());
            }
            return s.ToString();
        }

        //operatorii relationali == != < <= > >=  
        public static bool operator ==(NumarMare n1, NumarMare n2)
        {
            if (n1.lenght == n2.lenght && n1.sign == n2.sign)
            {
                for (int i = 0; i < n1.lenght; i++)
                {
                    if (n1.v[i] != n2.v[i])
                        return false;
                }
                return true;
            }
            return false;
        }
        public static bool operator !=(NumarMare n1, NumarMare n2)
        {
            if (n1 == n2)
                return false;
            return true;
        }
        public static bool operator >(NumarMare n1, NumarMare n2)
        {
            if (n1 == n2)
                return false;
            if (n1.sign == n2.sign)
            {
                if (n1.sign == '+')
                {
                    if (n1.lenght > n2.lenght)
                        return true;
                    if (n1.lenght == n2.lenght)
                    {
                        for (int i = n1.lenght - 1; i >= 0; i--)
                        {
                            if (n1.v[i] < n2.v[i])
                                return false;
                        }
                        return true;
                    }
                    return false;
                }
                if (n1.sign == '-')
                {
                    if (n1.lenght > n2.lenght)
                        return false;
                    if (n1.lenght == n2.lenght)
                    {
                        for (int i = n1.lenght - 1; i >= 0; i--)
                        {
                            if (n1.v[i] > n2.v[i])
                                return false;
                        }
                        return true;
                    }
                    return true;
                }
            }
            if (n1.sign == '+' && n2.sign == '-')
                return true;
            return false;
                
        }
        public static bool operator >=(NumarMare n1, NumarMare n2)
        {
            if (n1 > n2 || n1 == n2)
                return true;
            return false;
        }
        public static bool operator <(NumarMare n1, NumarMare n2)
        {
            if (n1 >= n2)
                return false;
            return true;
        }
        public static bool operator <=(NumarMare n1, NumarMare n2)
        {
            if (n1 < n2 || n1 == n2)
                return true;
            return false;
        }

        static int minim(int lenght1, int lenght2)
        {
            if (lenght1 >= lenght2)
                return lenght2;
            return lenght1;
        }

        //operatorii algebrici + - * /
        public static NumarMare operator +(NumarMare n1, NumarMare n2)
        {
            if (n1.sign == n2.sign)
            {
                int rest = 0;
                NumarMare rez = new NumarMare();
                int lungime_minima = minim(n1.lenght, n2.lenght);
                int[] max_number = n1.lenght > n2.lenght ? n1.v : n2.v;
                Array.Resize(ref rez.v, max_number.Length + 1);

                for (int i = 0; i < lungime_minima; i++)
                {
                    rez.v[i] += rest;
                    rez.v[i] += (n1.v[i] + n2.v[i]) % 10;
                    rest = (n1.v[i] + n2.v[i]) / 10;
                }
                for (int i = lungime_minima; i < max_number.Length; i++)
                {
                    rez.v[i] += rest;
                    rez.v[i] = (rez.v[i] + max_number[i]) % 10;
                    rest = (rez.v[i] + max_number[i]) / 10;
                }
                if (rest != 0)
                {
                    rez.v[max_number.Length] += rest;
                    rez.lenght = max_number.Length + 1;
                }
                else
                {
                    Array.Resize(ref rez.v, n1.lenght + n2.lenght - lungime_minima);
                    rez.lenght = max_number.Length + 2;
                }
                rez.sign = n1.sign;
                return rez;
            }
            if(n1.sign != n2.sign)
            {
                if (n1 == n2)
                    return new NumarMare();
                if (n1 > n2)
                {
                    NumarMare rez = new NumarMare();
                    rez = n1 - n2;
                    rez.sign = n1.sign;
                    return rez;
                }
                if(n1 < n2)
                {
                    NumarMare rez = new NumarMare();
                    rez = n2 - n1;
                    rez.sign = n2.sign;
                    return rez;
                }
            }
            return new NumarMare();
        }
        public static NumarMare operator -(NumarMare n1, NumarMare n2)
        {
            if(n1.sign != n2.sign)
            {
                NumarMare rez = new NumarMare();
                rez.sign = n1.sign;
                rez = n1 + n2;
            }
            if(n1.sign == n2.sign)
            {
                if (n1 == n2)
                {
                    return new NumarMare();
                }
                int imprumut = 0;
                NumarMare rez = new NumarMare();
                int lungime_minima = minim(n1.lenght, n2.lenght);
                int[] max_lenght = n1.lenght > n2.lenght ? n1.v : n2.v;
                Array.Resize(ref rez.v, max_lenght.Length);

                for (int i = 0; i < lungime_minima; i++)
                {
                    n1.v[i] -= imprumut;
                    imprumut = 0;
                    if(n1.v[i] - n2.v[i] >= 0)
                    {
                        rez.v[i] = n1.v[i] - n2.v[i];
                    }
                    else
                    {
                        imprumut++;
                        rez.v[i] = (10 + n1.v[i]) - n2.v[i];
                    }
                }
                for (int i = lungime_minima; i < max_lenght.Length; i++)
                {
                    if(imprumut != 0)
                    {
                        if (max_lenght[i] != 0)
                        {
                            max_lenght[i] -= imprumut;
                            imprumut = 0;
                        }
                        else
                        {
                            max_lenght[i] = 10 - imprumut;
                            imprumut = 1;
                        }
                    }
                    rez.v[i] = max_lenght[i];
                }

                if (n1 < n2)
                {
                    rez.sign = n2.sign;
                }
                if (n1 > n2)
                {
                    rez.sign = n1.sign;
                }
                return rez;
            }
            return new NumarMare();
        } 
    }
}
