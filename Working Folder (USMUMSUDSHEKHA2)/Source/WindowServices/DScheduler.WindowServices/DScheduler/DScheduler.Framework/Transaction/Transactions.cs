using System;
using System.Transactions;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class EnlistmentNotification : IEnlistmentNotification
    {
        private readonly IResourceManager _resourceManager;

        /// <summary>
        ///
        /// </summary>
        /// <param name="resourceManager"></param>
        public EnlistmentNotification(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        /// <summary>
        /// Notifies an enlisted object that a transaction is being committed.
        /// </summary>
        /// <param name="enlistment">An <see cref="T:System.Transactions.Enlistment"/> object used to send a response to the transaction manager.</param>
        public void Commit(Enlistment enlistment)
        {
            Console.WriteLine("Commit notification received");

            //Do any work necessary when commit notification is received
            _resourceManager.Commit();

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

            //Do any work necessary when indout notification is received
            _resourceManager.InDoubt();

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
            var result = _resourceManager.Prepare();

            switch (result)
            {
                case Vote.None:
                    break;

                case Vote.Commit:

                    //If work finished correctly, reply prepared
                    preparingEnlistment.Prepared();
                    break;

                case Vote.Rollback:

                    // otherwise, do a ForceRollback
                    preparingEnlistment.ForceRollback(); break;

                default:

                    //should never reach this
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Notifies an enlisted object that a transaction is being rolled back (aborted).
        /// </summary>
        /// <param name="enlistment">A <see cref="T:System.Transactions.Enlistment"/> object used to send a response to the transaction manager.</param>
        public void Rollback(Enlistment enlistment)
        {
            Console.WriteLine("Rollback notification received");

            //Do any work necessary when rollback notification is received
            _resourceManager.Rollback();

            //Declare done on the enlistment
            enlistment.Done();
        }
    }
}