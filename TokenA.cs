namespace MetodosCerrados
{
    public class TokenA : Token, IDisposable
    {
        private StreamReader archivo;
        private StreamWriter log;
        public TokenA()
        {
            log = new StreamWriter("tokens.log");
            log.AutoFlush = true;
            archivo = new StreamReader("Operaciones.log");
        }
        public TokenA(string nombre)
        {
            log = new StreamWriter(Path.GetFileNameWithoutExtension(nombre) + ".log");
            log.AutoFlush = true;
            archivo = new StreamReader(nombre);
        }
        public void Dispose() // ~TokenA
        {
            archivo.Close();
            log.Close();
        }

        public void nextToken()
        {
            char c;
            string buffer = "";
            // a) Buscar el primer caracter válido
            while (char.IsWhiteSpace(c = (char)archivo.Read()))
            {
            }
            buffer += c; // buffer = buffer + c;
            if (char.IsDigit(c))
            {
                setClasificacion(TiposToken.Numero);
                while (char.IsDigit(c = (char)archivo.Peek()))
                {
                    buffer += c;
                    archivo.Read();
                }
                if (c == '.')
                {
                    // Parte fraccional
                    archivo.Read();
                    buffer += c;
                    if (char.IsDigit(c = (char)archivo.Peek()))
                    {
                        while (char.IsDigit(c = (char)archivo.Peek()))
                        {
                            buffer += c;
                            archivo.Read();
                        }
                    }
                }
                if (char.ToLower(c) == 'e')
                {
                    archivo.Read();
                    buffer += c;
                    if (char.Equals(c = (char)archivo.Peek(), '+'))
                    {
                        archivo.Read();
                        buffer += c;
                    }
                    else if (char.Equals(c = (char)archivo.Peek(), '-'))
                    {
                        archivo.Read();
                        buffer += c;
                    }
                    if (char.IsDigit(c = (char)archivo.Peek()))
                    {
                        while (char.IsDigit(c = (char)archivo.Peek()))
                        {
                            buffer += c;
                            archivo.Read();
                        }
                    }
                }
            }
            else if (c == '+')
            {
                setClasificacion(TiposToken.Suma);
            }
            else if (c == '-')
            {
                setClasificacion(TiposToken.Resta);
            }
            else if (c == '*')
            {
                setClasificacion(TiposToken.Multiplicacion);
            }
            else if (c == '/')
            {
                setClasificacion(TiposToken.Division);
            }
            else if (c == '(')
            {
                setClasificacion(TiposToken.Miembro);
                while(!char.Equals(c = (char)archivo.Peek(), ')'))
                {
                    archivo.Read();
                    buffer += c;
                }
            }
            setContenido(buffer);
            log.WriteLine(getContenido() + " = " + getClasificacion());
        }
        
        public bool finArchivo()
        {
            return archivo.EndOfStream;
        }
    }
}