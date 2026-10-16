using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;

namespace SAMSBusinessLayer.Classes
{
    /// <summary>
    /// Class For Town Assignment Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Inserts Town Assignment
    /// </item>
    /// <term>
    /// Deletes Town Assignment
    /// </term>
    /// <item>
    /// Get Town Assignment
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
	public class DistributorTownController
	{		
		#region Constructors

        /// <summary>
        /// Constructor for DistributorTownController
        /// </summary>
		public DistributorTownController()
		{
			
		}

		#endregion
				
		#region Private Variables
		
		#endregion

		#region public Properties
		
		#endregion
		
		#region public static Methods
		#endregion

		#region Private Methods
		#endregion
		
		#region public Methods

        /// <summary>
        /// Inserts Town Assignment
        /// </summary>
        /// <remarks>
        /// Returns "Record Inserted" on Success And Null On Failure
        /// </remarks>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Town_Id">Town</param>
        /// <param name="p_Is_Active">IsActive</param>
        /// <param name="p_AREA_Id">Area</param>
        /// <param name="p_Ip_Address">IP</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <returns>"Record Inserted" on Success And Null On Failure</returns>
        public string InsertDistributorTown(int p_Distributor_Id, int p_Town_Id, bool p_Is_Active, int p_AREA_Id, string p_Ip_Address, int p_UserId) 
		{
			IDbConnection mConnection = null;
			try
			{
				mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
				mConnection.Open();
				spInsertDISTRIBUTOR_TOWN mDistributor = new spInsertDISTRIBUTOR_TOWN();
				mDistributor.Connection = mConnection;
				
				mDistributor.DISTRIBUTOR_ID = p_Distributor_Id;
				mDistributor.TOWN_ID = p_Town_Id;
				mDistributor.IS_ACTIVE = p_Is_Active;
				mDistributor.USER_ID = p_UserId;
                mDistributor.TIME_STAMP = System.DateTime.Now ;
				mDistributor.LASTUPDATE_DATE = System.DateTime.Now ;
				mDistributor.IP_ADDRESS = p_Ip_Address;
                
				mDistributor.ExecuteQuery();
				return "Record Inserted";
				
			}
			catch(Exception exp)
			{
				ExceptionPublisher.PublishException(exp);				
				return null;
			}
			finally
			{
				if(mConnection != null && mConnection.State == ConnectionState.Open)
				{
					mConnection.Close();
				}
			}
			
		}
        public DataTable SelectTownAsignUnasignForClassificaition(int p_PARAENT_ID, int P_Pricipal_id, int TypeId, int p_ClasificatoinId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspGetAssignTownClsification mGeoHierarchy = new UspGetAssignTownClsification();
                mGeoHierarchy.Connection = mConnection;
                mGeoHierarchy.PARENT_ID = p_PARAENT_ID;
                mGeoHierarchy.PRINCIPAL_ID = P_Pricipal_id;
                mGeoHierarchy.TYPE = TypeId;
                mGeoHierarchy.CLASSIFICATON_ID = p_ClasificatoinId;
                DataTable dt = mGeoHierarchy.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;

            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public DataTable SelectTownClassification(int p_PrincipalId, int p_UserId, int TypeId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                SP_SELECT_TOWNCLASFICATION_NAME mGeoHierarchy = new SP_SELECT_TOWNCLASFICATION_NAME();
                mGeoHierarchy.Connection = mConnection;
                mGeoHierarchy.PRINCIPAL_ID = p_PrincipalId;
                mGeoHierarchy.USER_ID = p_UserId;
                mGeoHierarchy.TYPE = TypeId;
                DataTable dt = mGeoHierarchy.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;

            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public string InsertTownClassicationAssign(int p_PrincipalId, int p_Distributor_Id, int p_regionId
         , int p_zonId, int p_teritoryId, int p_Town_Id, bool p_Is_Active, bool p_isdeleted, int p_UserId, DateTime pDocumentDate)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                SP_INSERT_TOWN_CLASIFICATION mDistributor = new SP_INSERT_TOWN_CLASIFICATION();
                mDistributor.Connection = mConnection;
                mDistributor.PRINCIPAL_ID = p_PrincipalId;
                mDistributor.DISTRIBUTOR_ID = p_Distributor_Id;
                mDistributor.REGION_ID = p_regionId;
                mDistributor.ZONE_ID = p_zonId;
                mDistributor.TERITORY_ID = p_teritoryId;
                mDistributor.TOWN_ID = p_Town_Id;
                mDistributor.IS_ACTIVE = p_Is_Active;
                mDistributor.IS_DELETED = p_isdeleted;
                mDistributor.USER_ID = p_UserId;
                mDistributor.TIME_STAMP = pDocumentDate;
                mDistributor.DATE_MODIFIED = System.DateTime.Now;

                mDistributor.ExecuteQuery();
                return "Record Inserted";

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public string DeleteTwonClassification(int p_PrincipalId, int p_Town_Id, int p_classificationId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spDeleteTOWN_CLASSIFICATION mDistributor = new spDeleteTOWN_CLASSIFICATION();
                mDistributor.Connection = mConnection;

                mDistributor.PRINCIPAL_ID = p_PrincipalId;
                mDistributor.TOWN_ID = p_Town_Id;
                mDistributor.CLASSIFICATION_ID = p_classificationId;
                mDistributor.ExecuteQuery();
                return "Record Deleted.";
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public string InsertClassificationName(int p_Distributor_Id, bool p_Is_Active, int p_principalId, string p_classificationName, int p_UserId, DateTime p_DocumentDate)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                SP_INSERT_TOWNCLASSIFICATION_NEW mDistributor = new SP_INSERT_TOWNCLASSIFICATION_NEW();
                mDistributor.Connection = mConnection;

                mDistributor.DISTRIBUTOR_ID = p_Distributor_Id;

                mDistributor.IS_ACTIVE = p_Is_Active;
                mDistributor.USER_ID = p_UserId;
                mDistributor.TIME_STAMP = p_DocumentDate;
                mDistributor.DATE_MODIFIED = System.DateTime.Now;
                mDistributor.CLASSIFICATION_NAME = p_classificationName;
                mDistributor.PRINCIPAL_ID = p_principalId;

                mDistributor.ExecuteQuery();
                return "Record Inserted";

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        /// <summary>
        /// Deletes Town Assignment
        /// </summary>
        /// <remarks>
        /// Returns "Record Deleted." on Success And Null on Failure
        /// </remarks>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Town_Id">Town</param>
        /// <returns>"Record Deleted." on Success And Null on Failure</returns>
	    public string DeletedDistributorTown(int p_Distributor_Id, int p_Town_Id)
		{			
			IDbConnection mConnection = null;
			try
			{
				mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
				mConnection.Open();
				spDeleteDISTRIBUTOR_TOWN mDistributor = new spDeleteDISTRIBUTOR_TOWN ();
				mDistributor.Connection = mConnection;

				mDistributor.DISTRIBUTOR_ID = p_Distributor_Id;
				mDistributor.TOWN_ID = p_Town_Id;
                
				mDistributor.ExecuteQuery();
				return "Record Deleted.";
			}
			catch(Exception exp)
			{
				ExceptionPublisher.PublishException(exp);				
				return null;
			}
			finally
			{
				if(mConnection != null && mConnection.State == ConnectionState.Open)
				{
					mConnection.Close();
				}
			}
			
		}

        /// <summary>
        /// Gets Town Data
        /// </summary>
        /// <param name="p_PARAENT_ID">Territory</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="TypeId">Type</param>
        /// <returns>Town Data As Datatable</returns>
        public DataTable SelectAssignTown(int p_PARAENT_ID, int p_DISTRIBUTOR_ID, int TypeId)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspGetAssignTown mGeoHierarchy = new UspGetAssignTown();
                mGeoHierarchy.Connection = mConnection;
                mGeoHierarchy.PARENT_ID = p_PARAENT_ID;
                mGeoHierarchy.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mGeoHierarchy.TYPE = TypeId;
                DataTable dt = mGeoHierarchy.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;

            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public bool Insert_FieldForceMapping(int p_Designation_ID, int p_Employee_ID, int p_Assigned_Employee_ID, int p_UserId, DateTime p_TIMESTAMP, bool p_IS_deleted, IDbConnection mConnection, IDbTransaction mTransaction)
        {
            try
            {
                spInsertUSER_ASSIGNMENT ObjInsert = new spInsertUSER_ASSIGNMENT();
                ObjInsert.Connection = mConnection;
                ObjInsert.Transaction = mTransaction;
                ObjInsert.DESIGNATION_ID = p_Designation_ID;
                ObjInsert.USER_ID = p_Employee_ID;
                ObjInsert.ASSIGNED_USER_ID = p_Assigned_Employee_ID;

                ObjInsert.TIME_STAMP = p_TIMESTAMP;
                ObjInsert.LAST_UPDATED = p_TIMESTAMP;
                ObjInsert.PUNCH_BY_USER_ID = p_UserId;
                ObjInsert.IS_DELETED = false;
                return ObjInsert.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }

        }

        public bool Delete_FieldForceMapping(int p_Designation_ID, int p_Employee_ID, IDbConnection mConnection, IDbTransaction mTransaction)
        {
            try
            {
                spDeleteUSER_ASSIGNMENT ObjInsert = new spDeleteUSER_ASSIGNMENT();
                ObjInsert.Connection = mConnection;
                ObjInsert.Transaction = mTransaction;
                ObjInsert.DESIGNATION_ID = p_Designation_ID;
                ObjInsert.USER_ID = p_Employee_ID;

                return ObjInsert.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }

        }

        public DataTable Select_FieldForceMapping(int p_Designation_Id, int p_Employee_Id, int p_Sp_Type)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spSelectUSER_ASSIGNMENT ObjInsert = new spSelectUSER_ASSIGNMENT();

                ObjInsert.Connection = mConnection;

                ObjInsert.DESIGNATION_ID = p_Designation_Id;
                ObjInsert.USER_ID = p_Employee_Id;
                ObjInsert.SP_TYPE = p_Sp_Type;

                DataTable dt = ObjInsert.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;

            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public bool Insert_DSR_MAPPING(int p_NSM_ID, int p_RSM_ID, int p_ASM_ASO_ID, int p_TSO_TSE_ID, int p_DISTRIBUTOR_ID,
          int p_DSR_ID, int p_UserId, DateTime p_TIMESTAMP, bool p_ISDELETED, IDbConnection mConnection, IDbTransaction mTransaction)
        {
            try
            {
                spInsertDSR_ASSIGNMENT ObjInsert = new spInsertDSR_ASSIGNMENT();
                ObjInsert.Connection = mConnection;
                ObjInsert.Transaction = mTransaction;
                ObjInsert.NSM_ID = p_NSM_ID;
                ObjInsert.RSM_ID = p_RSM_ID;
                ObjInsert.ASM_ASO_ID = p_ASM_ASO_ID;
                ObjInsert.TSO_TSE_ID = p_TSO_TSE_ID;
                ObjInsert.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                ObjInsert.ASSIGNED_DSR_ID = p_DSR_ID;
                ObjInsert.TIME_STAMP = p_TIMESTAMP;
                ObjInsert.IS_DELETED = p_ISDELETED;
                ObjInsert.PUNCH_BY_USER_ID = p_UserId;
                ObjInsert.LAST_UPDATED = p_TIMESTAMP;
                return ObjInsert.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }

        }

        public bool Delete_DSRMapping(int p_NSM_ID, int p_RSM_ID, int p_ASM_ASO_ID, int p_TSO_TSE_ID, int p_DISTRIBUTOR_ID,
            int p_DSR_ID, IDbConnection mConnection, IDbTransaction mTransaction)
        {
            try
            {
                spDeleteDSR_ASSIGNMENT ObjInsert = new spDeleteDSR_ASSIGNMENT();
                ObjInsert.Connection = mConnection;
                ObjInsert.Transaction = mTransaction;
                ObjInsert.NSM_ID = p_NSM_ID;
                ObjInsert.RSM_ID = p_RSM_ID;
                ObjInsert.ASM_ASO_ID = p_ASM_ASO_ID;
                ObjInsert.TSO_TSE_ID = p_TSO_TSE_ID;
                ObjInsert.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                ObjInsert.ASSIGNED_DSR_ID = p_DSR_ID;

                return ObjInsert.ExecuteQuery();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }

        }


        public DataTable Select_DSR_Mapping(int p_NSM_ID, int p_RSM_ID, int p_ASM_ASO_ID, int p_TSO_TSE_ID, int p_DISTRIBUTOR_ID,
            int p_DSR_ID, int p_Sp_Type)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spSelectDSR_ASSIGNMENT ObjInsert = new spSelectDSR_ASSIGNMENT();

                ObjInsert.Connection = mConnection;

                ObjInsert.NSM_ID = p_NSM_ID;
                ObjInsert.RSM_ID = p_RSM_ID;
                ObjInsert.ASM_ASO_ID = p_ASM_ASO_ID;
                ObjInsert.TSO_TSE_ID = p_TSO_TSE_ID;
                ObjInsert.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                ObjInsert.ASSIGNED_DSR_ID = p_DSR_ID;
                ObjInsert.SP_TYPE = p_Sp_Type;

                DataTable dt = ObjInsert.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;

            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #endregion
    }
}
