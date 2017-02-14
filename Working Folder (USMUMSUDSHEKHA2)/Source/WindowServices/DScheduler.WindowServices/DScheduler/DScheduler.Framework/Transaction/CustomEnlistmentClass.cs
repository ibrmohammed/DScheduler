using System;
using System.Transactions;

namespace DScheduler.Framework
{
    /// <summary>
    /// Custom Enlistment Class to test
    /// </summary>
    public class CustomEnlistmentClass : IEnlistmentNotification
    {
        private readonly bool _successful;

        /// <summary>
        ///
        /// </summary>
        /// <param name="successful"></param>
        public CustomEnlistmentClass(bool successful)
        {
            _successful = successful;
        }

        /// <summary>
        /// Notifies an enlisted object that a transaction is being committed.
        /// </summary>
        /// <param name="enlistment">An <see cref="T:System.Transactions.Enlistment"/> object used to send a response to the transaction manager.</param>
        public void Commit(Enlistment enlistment)
        {
            Console.WriteLine("Commit notification received");

            //Do any work necessary when commit notification is received

            //Declare done on the enlistment
            enlistment.Done();
        }

        /// <summary>
        /// Notifies an enlisted object that the status of a transaction is in doubt.
        /// </summary>
        /// <param name="enlistment">An <see cref="T:System.Transactions.Enlistment"/> object used to send a response to the transaction manager.</param>
        public void InDoubt(Enlistment enlistment)
        {
            Console.WriteLine("In doubt notification received");

            //Do any work necessary when indoubt notification is received

            //Declare done on the enlistment
            enlistment.Done();
        }

        /// <summary>
        /// Notifies an enlisted object that a transaction is being prepared for commitment.
        /// </summary>
        /// <param name="preparingEnlistment">A <see cref="T:System.Transactions.PreparingEnlistment"/> object used to send a response to the transaction manager.</param>
        public void Prepare(PreparingEnlistment preparingEnlistment)
        {
            Console.WriteLine("Prepare notification received");

            //Perform transactional work

            if (_successful)

                //If work finished correctly, reply prepared
                preparingEnlistment.Prepared();
            else

                // otherwise, do a ForceRollback
                preparingEnlistment.ForceRollback();
        }

        /// <summary>
        /// Notifies an enlisted object that a transaction is being rolled back (aborted).
        /// </summary>
        /// <param name="enlistment">A <see cref="T:System.Transactions.Enlistment"/> object used to send a response to the transaction manager.</param>
        public void Rollback(Enlistment enlistment)
        {
            Console.WriteLine("Rollback notification received");

            //Do any work necessary when rollback notification is received

            //Declare done on the enlistment
            enlistment.Done();
        }
    }
}