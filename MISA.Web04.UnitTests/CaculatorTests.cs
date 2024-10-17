
namespace MISA.Web04.Api.UnitTests
{
    [TestFixture]
    public class CaculatorTests
    {
        

        [TestCase(4,5,9)]
        [TestCase(1,1,2)]
        [TestCase(int.MaxValue, 1, (long)int.MaxValue+1)]
        
        public void Add_ValidInput_Success(int a, int b, long expectedResult)
        {
            // Arrange
            
            // Act
            var actualResult = new Caculator().Add(a,b);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [TestCase(4, 5, -1)]
        [TestCase(1, 1, 0)]
        [TestCase(int.MaxValue,int.MinValue, (long)2*int.MaxValue)]
        public void Sub_ValidInput_Success(int a, int b, long expectedResult)
        {
            // Arrange

            // Act
            var actualResult = new Caculator().Sub(a, b);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [TestCase(4, 5, 20)]
        [TestCase(1, 1, 1)]
        [TestCase(int.MaxValue, int.MinValue, (long)int.MinValue * int.MaxValue)]
        public void Mul_ValidInput_Success(int a, int b, long expectedResult)
        {
            // Arrange

            // Act
            var actualResult = new Caculator().Mul(a, b);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [TestCase(4, 5, 0.8)]
        [TestCase(1, 1, 1)]
        [TestCase(5, 3, 1.666667)]
        public void Div_ValidInput_Success(int a, int b, double expectedResult)
        {
            // Arrange

            // Act
            var actualResult = new Caculator().Div(a, b);

            // Assert 
            Assert.That(Math.Abs(actualResult - expectedResult), Is.LessThan(10e-6));
        }

        [Test]
        public void Div_InalidInput_Exception()
        {
            // Arrange
            var a = 5;
            var b = 0;
            var expectedException = new Exception("Không ðý?c chia cho 0");
            // Act && assert
            var handler = () => new Caculator().Div(a, b);
            var actualExeption = Assert.Throws<Exception>(() => handler(),expectedException.Message);

        }
    }
}