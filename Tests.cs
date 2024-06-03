//using NUnit.Framework;

////тесты не работают, т.к. при установке Microsoft.NET.Test.Sdk, появляется ошибка,
////сообщающая о присутствии второго метода Main

//namespace game;

//[TestFixture]
//internal class tests
//{
//    [TestCase(new []{1, 2, 3, 4})]
//    public void TestCorrectGeneration(int[] values)
//    {
//        List<Projectile> Projectiles = new List<Projectile>();
        
//        for (int j = 0; j < 4; j++)
//        {
//            var proj = new Projectile();
//            Projectiles.Add(proj);
//        }

//        int i = 0;
//        foreach (var proj in Projectiles)
//        {
//            proj.Number = i;
//            proj.X = 1900 + 480 * i;
//            proj.SetHeight();
//            proj.SetValue(values[i]);
//            i++;
//        }

//        var h1 = 0;
//        var h2 = 0;

//        for (int k = 0; k < 1; k++)
//        {
//            var flag = 0;
//            for (int ii = 0; ii < 2; ii++)
//            {
//                for (int jj = 0; jj < 2; jj++)
//                {
//                    for (int kk = 0; kk < 2; kk++)
//                    {
//                        for (int ll = 0; ll < 2; ll++)
//                        {
//                            var hps = new[] { h1, h2 };

//                            hps[ii] += Projectiles[0].Value;

//                            hps[jj] += Projectiles[1].Value;

//                            hps[kk] += Projectiles[2].Value;

//                            hps[ll] += Projectiles[3].Value;

//                            if (hps[1] > -100 && hps[0] < 100 && hps[1] < 100 && hps[0] > -100)
//                                flag += 1;
//                        }
//                    }
//                }
//            }
//            var result = flag > 1;
//            Assert.That(result, Is.True, "У игрока есть возможность победить.");
//        }
        
//    }
//}

