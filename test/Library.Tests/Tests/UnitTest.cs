namespace Library.Tests;

public class Tests
{
    //Nuesta idea en general es dependiendo como lleguemos a las proximas entregas, ver si podemos hacer una conexion basica a una base de datos.
    //Si llegariamos a hacer eso, estuvimos averiguando que se puede hacer con un string de conexión y ese string lo podriamos sacar de una base de datos en SQL server. Esto estaria muy bueno
    //debido a que le da un poco mas de profesionalidad a nuestro proyecto y ya tenemos un lugar donde guardar los datos y que no sean harcodeados.
    //Tener en cuenta que la idea es hacer algo sencillo debido al margen de tiempo. Y ya de paso aprovechariamos para ir un poco más alla de lo que se plantea.
    
    //POr ultimo, para crear la base de datos y ganar tiempos, pensamos en usar Entity Framework, usando Code First para crear la base de datos y no hacerla manualmente(Ya que esto lo vamos a ver bien en Base de datos).
    //IDEA EN GENERAL: NuestroProyecto(CodeFirst)-> BaseDeDatos(SQL server)
    
    //Tambien estamos pensando en utilizar el framework de X.Unit.net y utilizar y ayudarnos en cada Test usando el patrón que nos enseño Juana llamado "Patrón AAA".
    
    //Lo que tenemos hasta ahora en la primer entrega es el esqueleto del proyecto. Estos Tests serian el "arranque" de la idea que queremos proponer.
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}
