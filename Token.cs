namespace MetodosCerrados
{
    public class Token
    {
        public enum TiposToken 
        {
            Numero, Suma, Resta, Multiplicacion, Division, Miembro
        }
        private string contenido;
        private TiposToken clasificacion;

        public Token()
        {
            contenido = "N/A";
        }
        public void setContenido(string contenido)
        {
            this.contenido = contenido;
        }
        public void setClasificacion(TiposToken clasificacion)
        {
            this.clasificacion = clasificacion;
        }
        public string getContenido()
        {
            return this.contenido;
        }
        public TiposToken getClasificacion()
        {
            return this.clasificacion;
        }
    }
}