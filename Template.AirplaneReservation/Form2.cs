using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

namespace AirplaneReservation
{
    /// <summary>
    /// Summary description for Form2.
    /// </summary>
    public class Form2 : System.Windows.Forms.Form
    {
        #region GUI lists & controls
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListBox lstOutside;
        private ListBox lstInside;
        private System.Windows.Forms.ListBox lstPlaneA;
        private System.Windows.Forms.ListBox lstPlaneB;
        private System.Windows.Forms.ListBox lstNotReserved;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Label label5;
        private Button btnPause;
        private GroupBox grpReservOffice;
        #endregion

        #region Global Variables and Shared Resources Declarations

        int NumOfCustomers = 100;
        int NumOfClerks = 3;


        int PlaneASeatsCount = 10;
        int PlaneBSeatsCount = 15;
        int MaxCapacity = 20;
        Queue customerIDQ, tripTypeQ;

        int CustomerCounter;

        ThreadStart TH;
        Thread[] CustomersThreads;
        Thread[] ClerksThreads;

        Semaphore Cust_ID;
        Semaphore S_lstOutside;
        Semaphore S_lstInside;
        Semaphore S_cap;
        Semaphore S_enter_service;
        Semaphore S_complete_service;
        Semaphore clerk_work;
        Semaphore clerk_wait;

        #endregion

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new Form2());
        }

        public Form2()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lstPlaneB = new System.Windows.Forms.ListBox();
            this.lstPlaneA = new System.Windows.Forms.ListBox();
            this.lstOutside = new System.Windows.Forms.ListBox();
            this.lstNotReserved = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lstInside = new System.Windows.Forms.ListBox();
            this.btnPause = new System.Windows.Forms.Button();
            this.grpReservOffice = new System.Windows.Forms.GroupBox();
            this.grpReservOffice.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(492, 392);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(96, 37);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(270, 392);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(96, 37);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lstPlaneB
            // 
            this.lstPlaneB.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPlaneB.ItemHeight = 22;
            this.lstPlaneB.Location = new System.Drawing.Point(305, 55);
            this.lstPlaneB.Name = "lstPlaneB";
            this.lstPlaneB.Size = new System.Drawing.Size(151, 290);
            this.lstPlaneB.TabIndex = 5;
            // 
            // lstPlaneA
            // 
            this.lstPlaneA.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPlaneA.ItemHeight = 22;
            this.lstPlaneA.Location = new System.Drawing.Point(157, 55);
            this.lstPlaneA.Name = "lstPlaneA";
            this.lstPlaneA.Size = new System.Drawing.Size(135, 290);
            this.lstPlaneA.TabIndex = 4;
            // 
            // lstOutside
            // 
            this.lstOutside.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstOutside.ItemHeight = 22;
            this.lstOutside.Location = new System.Drawing.Point(10, 48);
            this.lstOutside.Name = "lstOutside";
            this.lstOutside.Size = new System.Drawing.Size(152, 312);
            this.lstOutside.TabIndex = 8;
            // 
            // lstNotReserved
            // 
            this.lstNotReserved.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstNotReserved.ItemHeight = 22;
            this.lstNotReserved.Location = new System.Drawing.Point(666, 48);
            this.lstNotReserved.Name = "lstNotReserved";
            this.lstNotReserved.Size = new System.Drawing.Size(166, 312);
            this.lstNotReserved.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 23);
            this.label1.TabIndex = 10;
            this.label1.Text = "Outside";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(159, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 23);
            this.label2.TabIndex = 11;
            this.label2.Text = "A. CAI-BER";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(307, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 23);
            this.label3.TabIndex = 12;
            this.label3.Text = "B. BER - PAR";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(675, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 23);
            this.label4.TabIndex = 13;
            this.label4.Text = "Not Reserved";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(43, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 23);
            this.label5.TabIndex = 15;
            this.label5.Text = "Inside";
            // 
            // lstInside
            // 
            this.lstInside.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstInside.ItemHeight = 22;
            this.lstInside.Location = new System.Drawing.Point(10, 55);
            this.lstInside.Name = "lstInside";
            this.lstInside.Size = new System.Drawing.Size(135, 290);
            this.lstInside.TabIndex = 14;
            // 
            // btnPause
            // 
            this.btnPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPause.Location = new System.Drawing.Point(388, 392);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(88, 37);
            this.btnPause.TabIndex = 16;
            this.btnPause.Text = "Pause";
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // grpReservOffice
            // 
            this.grpReservOffice.Controls.Add(this.lstInside);
            this.grpReservOffice.Controls.Add(this.label5);
            this.grpReservOffice.Controls.Add(this.lstPlaneA);
            this.grpReservOffice.Controls.Add(this.lstPlaneB);
            this.grpReservOffice.Controls.Add(this.label2);
            this.grpReservOffice.Controls.Add(this.label3);
            this.grpReservOffice.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpReservOffice.Location = new System.Drawing.Point(174, 24);
            this.grpReservOffice.Name = "grpReservOffice";
            this.grpReservOffice.Size = new System.Drawing.Size(469, 362);
            this.grpReservOffice.TabIndex = 17;
            this.grpReservOffice.TabStop = false;
            this.grpReservOffice.Text = "Reservation Office";
            // 
            // Form2
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(841, 445);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstNotReserved);
            this.Controls.Add(this.lstOutside);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.grpReservOffice);
            this.Name = "Form2";
            this.Text = "Air-plane Reservation...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.grpReservOffice.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        void Customer()
        {
            Random Rnd = new Random(System.Environment.TickCount);
            Random Rnd2;
            int MyCnt;
            int ResType; //0: trip1, 1: trip2, 2: both trips

            //=================================================================
            //[TODO] TASK2: Complete the missing logic in Customers and Clerks
            //          1) Protect the shared resources 
            //          2) Handle the missing logic in customer [enter the office, exist the office]
            //          3) Handle the dependencies between both functions
            //=================================================================

            #region [TASK#2] [1] Take an ID and a randomly selected trip
            Cust_ID.Wait();
            MyCnt = CustomerCounter;
            //Just for randomization =========================
            Rnd2 = new Random(System.Environment.TickCount);
            Thread.Sleep(Rnd.Next(100));
            //================================================	
            CustomerCounter++;
            ResType = Rnd2.Next(2);
            resTypeArray[MyCnt] = ResType;
            Cust_ID.Signal();
            #endregion

            #region [TASK#2] [2] Customer is outside
            S_lstOutside.Wait();
            lstOutside.Items.Add("Customer " + MyCnt.ToString());
            S_lstOutside.Signal();
            #endregion

            Thread.Sleep(Rnd.Next(700));
            //////////////////////////////////////////////////////////

            #region [TASK#2] [3] Customer enter the office
            S_cap.Wait();
            S_lstOutside.Wait();
            lstOutside.Items.Remove("Customer " + MyCnt.ToString());
            S_lstInside.Wait();
            lstInside.Items.Add("Customer " + MyCnt.ToString());
            S_lstInside.Signal();
            S_lstOutside.Signal();
            #endregion

            #region [TASK#3] [4] Place an order to the clerk [ID, desired trip]
            S_enter_service.Wait();
            customerIDQ.Enqueue(MyCnt);
            tripTypeQ.Enqueue(ResType);
            S_enter_service.Signal();
            clerk_work.Signal();
           
            #endregion

            #region [TASK#2 & TASK#5] [4] Customer waits the clerk to finishes him/her
            S_complete_service.Wait();
            #region [TASK#2] Unfair solution

            #endregion

            #region [TASK#5] Fair solution
            
            #endregion

            #endregion

            #region [TASK#2] [5] Exist the office
            S_lstInside.Wait();
            lstInside.Items.Remove("Customer " + MyCnt.ToString());
            S_lstInside.Signal();
            S_cap.Signal();
            #endregion

            allFinished[MyCnt].Signal();
        }

        void Clerk()
        {
            Random Rnd = new Random(System.Environment.TickCount);
            int CustCnt = 0;
            int Reservation = -1;
            while (true)
            {
                #region [TASK#2] [1] Wait the customer 
                clerk_work.Wait();
                #endregion

                #region [TASK#3] [2] Take the order [ID, trip]
                clerk_wait.Wait();
                CustCnt = (int)customerIDQ.Dequeue();
                Reservation = (int)tripTypeQ.Dequeue();
                clerk_wait.Signal();
                #endregion

                #region [TASK#4] [3] Check if there's available place in the required trip
                if (Reservation == 0)
                {
                    //Reserve on TripA
                    //your code here
                    if (PlaneASeatsCount>0)
                    {
                        lstPlaneA.Items.Add("Customer " + CustCnt.ToString());
                        PlaneASeatsCount--;
                    }
                    else
                    {
                        lstNotReserved.Items.Add("Customer " + CustCnt.ToString());
                    }
                }
                else if (Reservation == 1)
                {
                    //Reserve on TripB
                    //your code here
                    //your code here
                    if (PlaneBSeatsCount > 0)
                    {
                        lstPlaneB.Items.Add("Customer " + CustCnt.ToString());
                        PlaneBSeatsCount--;
                    }
                    else
                    {
                        lstNotReserved.Items.Add("Customer " + CustCnt.ToString());
                    }
                }
                
                #endregion

                #region [TASK#2 & TASK#5] [4] Finish the customer
                S_complete_service.Signal();
                #region [TASK#2] Unfair solution

                #endregion

                #region [TASK#5] Fair solution
             
                #endregion

                #endregion

            }
        }


        private void btnStart_Click(object sender, System.EventArgs e)
        {
            CustomerCounter = 1;
            
            #region Clear Lists
            lstOutside.Items.Clear();
            lstNotReserved.Items.Clear();
            lstPlaneA.Items.Clear();
            lstPlaneB.Items.Clear();
            #endregion

            #region Semaphores Initialization [TODO]
            //Fill your code here...
            Cust_ID = new Semaphore(1);
            S_lstOutside = new Semaphore(1);
            S_lstInside = new Semaphore(1);
            S_cap = new Semaphore(MaxCapacity);
            S_enter_service = new Semaphore(1);
            S_complete_service = new Semaphore(0);
            clerk_work = new Semaphore(0);
            clerk_wait = new Semaphore(1);


            #endregion

            allFinished = new Semaphore[NumOfCustomers + 1];
            for (int i = 1; i <= NumOfCustomers; i++)
                allFinished[i] = new Semaphore(0);

            resTypeArray = new int[NumOfCustomers + 1];

            //===============================================================
            //[TODO] TASK1: Create and run the suitable number of threads
            //===============================================================

            #region [TASK#1] Threads Creation & Running
            //Create the suitable number of threads for Customers & Clerks
            //Use the following two arrays of threads
            //  1. Customers
            //  2. Clerks
            //Fill your code here...
            CustomersThreads = new Thread[NumOfCustomers];
            ClerksThreads = new Thread[NumOfClerks];



            #endregion

            if (validate())
            {
                MessageBox.Show("Congratulations !! You've passed all the tests (Y)");
            }
        }

        private void btnStop_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < NumOfCustomers; i++)
                CustomersThreads[i].Abort();

            for (int i = 0; i < NumOfClerks; i++)
                ClerksThreads[i].Abort();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (btnPause.Text == "Pause")
            {
                for (int i = 0; i < NumOfCustomers; i++)
                {
                    if (CustomersThreads[i].IsAlive)
                        CustomersThreads[i].Suspend();
                }
                for (int i = 0; i < NumOfClerks; i++)
                {
                    if (ClerksThreads[i].IsAlive)
                        ClerksThreads[i].Suspend();
                }
                btnPause.Text = "Resume";
            }
            else
            {
                for (int i = 0; i < NumOfCustomers; i++)
                {
                    if (CustomersThreads[i].IsAlive)
                        CustomersThreads[i].Resume();
                }
                for (int i = 0; i < NumOfClerks; i++)
                {
                    if (ClerksThreads[i].IsAlive)
                        ClerksThreads[i].Resume();
                }
                btnPause.Text = "Pause";
            }

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CustomersThreads == null || ClerksThreads == null)
                return;

            for (int i = 0; i < NumOfCustomers; i++)
            {
                if (CustomersThreads[i].IsAlive)
                    CustomersThreads[i].Abort();
            }
            for (int i = 0; i < NumOfClerks; i++)
                ClerksThreads[i].Abort();
        }

        //validation
        Semaphore[] allFinished;
        int[] resTypeArray;

        private bool validate()
        {
            //all customers reserved or left
            for (int i = 1; i <= NumOfCustomers; i++)
                allFinished[i].Wait();

            if (lstPlaneA.Items.Count + lstPlaneB.Items.Count + lstNotReserved.Items.Count != NumOfCustomers)
            {
                MessageBox.Show("Not all customers reserved or left !!");
                return false;
            }

            // no repeated id in any list
            for (int i = 0; i < lstPlaneA.Items.Count; i++)
            {
                for (int j = i + 1; j < lstPlaneA.Items.Count; j++)
                {
                    if (lstPlaneA.Items[i] == lstPlaneA.Items[j])
                    {
                        MessageBox.Show("Repeated Customer " + lstPlaneA.Items[i] + " in plane A !!");
                        return false;
                    }
                }
            }

            for (int i = 0; i < lstPlaneB.Items.Count; i++)
            {
                for (int j = i + 1; j < lstPlaneB.Items.Count; j++)
                {
                    if (lstPlaneB.Items[i] == lstPlaneB.Items[j])
                    {
                        MessageBox.Show("Repeated Customer " + lstPlaneB.Items[i] + " in plane B !!");
                        return false;
                    }
                }
            }

            for (int i = 0; i < lstNotReserved.Items.Count; i++)
            {
                for (int j = i + 1; j < lstNotReserved.Items.Count; j++)
                {
                    if (lstNotReserved.Items[i] == lstNotReserved.Items[j])
                    {
                        MessageBox.Show("Repeated Customer " + lstNotReserved.Items[i] + " in the list of not reserved !!");
                        return false;
                    }
                }
            }

            //no customer in not reserved and reserved in the same time
            for (int i = 0; i < lstNotReserved.Items.Count; i++)
            {
                for (int j = 0; j < lstPlaneA.Items.Count; j++)
                {
                    if (lstNotReserved.Items[i] == lstPlaneA.Items[j])
                    {
                        MessageBox.Show("Customer " + lstNotReserved.Items[i] + " is reserved A and not reserved at the same time !!");
                        return false;
                    }
                }

                for (int j = 0; j < lstPlaneB.Items.Count; j++)
                {
                    if (lstNotReserved.Items[i] == lstPlaneB.Items[j])
                    {
                        MessageBox.Show("Customer " + lstNotReserved.Items[i] + " is reserved B and not reserved at the same time !!");
                        return false;
                    }
                }

            }

            //no customer in both A and B
            for (int i = 0; i < lstPlaneA.Items.Count; i++)
            {
                for (int j = 0; j < lstPlaneB.Items.Count; j++)
                {
                    if (lstPlaneA.Items[i] == lstPlaneB.Items[j])
                    {
                        MessageBox.Show("Customer " + lstPlaneB.Items[i] + " is reserved twice in plane A and B !!");
                        return false;
                    }
                }
            }

            // customers reserved the same thing they requested
            for (int i = 0; i < lstPlaneA.Items.Count; i++)
            {
                int customerId = int.Parse(lstPlaneA.Items[i].ToString());
                if (resTypeArray[customerId] == 1)
                {
                    MessageBox.Show("Customer " + customerId + " Requested type B only but reserved type A !!");
                    return false;
                }
            }

            for (int i = 0; i < lstPlaneB.Items.Count; i++)
            {
                int customerId = int.Parse(lstPlaneB.Items[i].ToString());
                if (resTypeArray[customerId] == 0)
                {
                    MessageBox.Show("Customer " + customerId + " Requested type A only but reserved type B !!");
                    return false;
                }
            }

            //check that there are not reserved customers while there are remaining places
            for (int i = 0; i < lstNotReserved.Items.Count; i++)
            {
                int customerId = int.Parse(lstNotReserved.Items[i].ToString());
                if (resTypeArray[customerId] == 0 && lstPlaneA.Items.Count < PlaneASeatsCount)
                {
                    MessageBox.Show("Customer " + customerId + " not reserved while there is available place !!");
                }
                else if (resTypeArray[customerId] == 1 && lstPlaneB.Items.Count < PlaneBSeatsCount)
                {
                    MessageBox.Show("Customer " + customerId + " not reserved while there is available place !!");
                }
            }

            return true;
        }
    }
}
