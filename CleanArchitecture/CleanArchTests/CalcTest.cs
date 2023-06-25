namespace CleanArchTests
{
    public class CalcTest
    {
        [Theory]
        [InlineData(10,10,20)]
        [InlineData(20,20,40)]
        [InlineData(15,35,50)]
        public void Sum_ShouldCalcAndReturnSameValue(int a,int b,int expectedResult) 
        {
            //Arrange

            //Act
            int actual = Sum(a, b);
            
            //Assert
            Assert.Equal(expectedResult, actual);
        }

        private static int Sum(int a, int b)
            => a + b;
    }
}
