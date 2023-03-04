namespace AJPPABLAB1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try{
                // Kod programu
            }
            catch(Exception ex){
                // Kod programu wykonywany jeśli w bloku wyżej zostanie wychwycony Exception.
                Console.WriteLine(ex.Message);
            }
            finally {
                // Blok kodu wykonywany zawsze, nawet jeśli zostanie wychwycony Exception albo nie.
            }

        }
    }
}