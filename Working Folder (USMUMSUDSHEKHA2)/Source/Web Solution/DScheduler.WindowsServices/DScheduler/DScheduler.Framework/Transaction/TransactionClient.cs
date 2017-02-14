using System;
using System.Transactions;

namespace DScheduler.Framework
{
    internal class TransactionClient
    {
        public static void Main(string[] args)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    //Create an enlistment object
                    var cohort = new EnlistmentNotification(new FileActionsManager(f => f.Copy()));

                    //Enlist on the current transaction with the enlistment object
                    Transaction.Current.EnlistVolatile(cohort, EnlistmentOptions.None);

                    //Perform transactional work here.

                    //Call complete on the TransactionScope based on console input
                    ConsoleKeyInfo c;
                    while (true)
                    {
                        Console.Write("Complete the transaction scope? [Y|N] ");
                        c = Console.ReadKey();
                        Console.WriteLine();

                        if ((c.KeyChar == 'Y') || (c.KeyChar == 'y'))
                        {
                            scope.Complete();
                            break;
                        }

                        if ((c.KeyChar == 'N') || (c.KeyChar == 'n'))
                        {
                            break;
                        }
                    }
                }
            }
            catch (TransactionException ex)
            {
                Console.WriteLine(ex);
            }
            catch
            {
                Console.WriteLine("Cannot complete transaction");
                throw;
            }
        }
    }
}