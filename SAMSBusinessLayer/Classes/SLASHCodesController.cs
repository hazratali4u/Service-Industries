using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;

namespace SAMSBusinessLayer.Classes
{
    /// <summary>
    /// Class For Slash Codes Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert Slash Codes
    /// </item>
    /// <term>
    /// Update Slash Codes
    /// </term>
    /// <item>
    /// Get Slash Codes
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
	public class SLASHCodesController
	{	
		#region Constructors

        /// <summary>
        /// Constructor for SLASHCodesController
        /// </summary>
		public SLASHCodesController()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#endregion
				
		#region public Methods

        #region Select

        /// <summary>
        /// Gets Slash Codes
        /// </summary>
        /// <remarks>
        /// Returns Slash Codes as Datatable
        /// </remarks>
        /// <param name="p_RefID">SlashCode</param>
        /// <param name="p_SlashCode">Code</param>
        /// <param name="p_Parent_Ref_ID">Parent</param>
        /// <param name="p_SlashDesc">Description</param>
        /// <param name="p_Status">Status</param>
        /// <param name="p_IsActive">IsActive</param>
        /// <returns>Slash Codes as Datatable</returns>
        public DataTable SelectSlashCodes(int p_RefID, string p_SlashCode, int p_Parent_Ref_ID, string p_SlashDesc, int p_Status, bool p_IsActive)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectCODES mSlashCodes = new spSelectCODES();
                mSlashCodes.Connection = mConnection;
                mSlashCodes.PARENT_REF_ID = p_Parent_Ref_ID;
                mSlashCodes.SLASH_CODE = p_SlashCode;
                mSlashCodes.SLASH_DESC = p_SlashDesc;
                mSlashCodes.TIME_STAMP = Constants.DateNullValue;
                mSlashCodes.STATUS = p_Status;
                mSlashCodes.REF_ID = p_RefID;
                mSlashCodes.IS_ACTIVE = p_IsActive;
                DataTable dt = mSlashCodes.ExecuteTable();
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

        #region Insert, Update

        /// <summary>
        /// Inserts Slash Code
        /// </summary>
        /// <remarks>
        /// Returns "Record Inserted" On Success And Exception.Message On Failure
        /// </remarks>
        /// <param name="p_SlashCode">SlashCode</param>
        /// <param name="p_Parent_Ref_ID">Parent</param>
        /// <param name="p_SlashDesc">Desctiption</param>
        /// <param name="p_Status">Status</param>
        /// <param name="p_IsActive">IsActive</param>
        /// <returns>"Record Inserted" On Success And Exception.Message On Failure</returns>
        public string InsertSlashCodes(string p_SlashCode, int p_Parent_Ref_ID, string p_SlashDesc, int p_Status, bool p_IsActive)
		{
			IDbConnection mConnection = null;
			try
			{
				mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
				spInsertCODES mSlashCodes = new spInsertCODES();
				mSlashCodes.Connection = mConnection;
				mSlashCodes.PARENT_REF_ID = p_Parent_Ref_ID;
				mSlashCodes.SLASH_CODE = p_SlashCode;
				mSlashCodes.SLASH_DESC = p_SlashDesc;
				mSlashCodes.TIME_STAMP = DateTime.Now;
				mSlashCodes.STATUS = p_Status;
				mSlashCodes.IS_ACTIVE = p_IsActive;
				mSlashCodes.ExecuteQuery();
				return "Record Inserted";
				
			}			
			catch(Exception exp)
			{
				ExceptionPublisher.PublishException(exp);				
				return exp.Message;
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
        /// Updates Slash Code
        /// </summary>
        /// <remarks>
        /// Returns "Record Updated" On Success And Exception.Message On Failure
        /// </remarks>
        /// <param name="p_RefID">SlashCode</param>
        /// <param name="p_SlashCode">Code</param>
        /// <param name="p_Parent_Ref_ID">Parent</param>
        /// <param name="p_SlashDesc">Description</param>
        /// <param name="p_Status">Status</param>
        /// <param name="p_IsActive">IsActive</param>
        /// <param name="p_Time_Stamp">CreatedOn</param>
        /// <returns>"Record Updated" On Success And Exception.Message On Failure</returns>
		public string UpdateSlashCodes(int p_RefID, string p_SlashCode, int p_Parent_Ref_ID, string p_SlashDesc, int p_Status, bool p_IsActive, DateTime p_Time_Stamp)
		{
			IDbConnection mConnection = null;
			try
			{
				mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
				mConnection.Open();
				spUpdateCODES mSlashCodes = new spUpdateCODES();
				mSlashCodes.Connection = mConnection;
				mSlashCodes.REF_ID = p_RefID;
				mSlashCodes.PARENT_REF_ID = p_Parent_Ref_ID;
				mSlashCodes.SLASH_CODE = p_SlashCode;
				mSlashCodes.SLASH_DESC = p_SlashDesc;
				mSlashCodes.TIME_STAMP = p_Time_Stamp;
				mSlashCodes.STATUS = p_Status;
				mSlashCodes.IS_ACTIVE = p_IsActive;

				mSlashCodes.ExecuteQuery();
				
				return "Record Updated";
			}
			catch(Exception exp)
			{
				ExceptionPublisher.PublishException(exp);				
				return exp.Message;
			}
			finally
			{
				if(mConnection != null && mConnection.State == ConnectionState.Open)
				{
					mConnection.Close();
				}
			}
        }


        public long setPJP(int p_Distributor_id, int p_ORDER_BOOKER_ID, int p_DATE_TYPE, DateTime p_EFECTIVE_START_DATE,
          DateTime p_EFFECTIVE_END_DATE, int p_USER_ID, bool p_IS_ACTIVE, int p_TYPE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spCRUDPJP mSlashCodes = new spCRUDPJP();
                mSlashCodes.Connection = mConnection;
                mSlashCodes.DISTRIBUTOR_ID = p_Distributor_id;
                mSlashCodes.ORDER_BOOKER_ID = p_ORDER_BOOKER_ID;
                mSlashCodes.DATE_TYPE = p_DATE_TYPE;
                mSlashCodes.EFECTIVE_START_DATE = p_EFECTIVE_START_DATE;
                mSlashCodes.TIME_STAMP = DateTime.Now;
                mSlashCodes.MODIFIED_DATE = DateTime.Now;
                mSlashCodes.EFFECTIVE_END_DATE = p_EFFECTIVE_END_DATE;
                mSlashCodes.USER_ID = p_USER_ID;
                mSlashCodes.IS_ACTIVE = p_IS_ACTIVE;
                mSlashCodes.TYPE_ID = p_TYPE_ID;

                bool chk = mSlashCodes.ExecuteQuery();
                long ret = 0;
                if (chk)
                {
                    ret = mSlashCodes.pjp_master_ID;
                }
                return ret;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return 0;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }


        }

        public int setPJPDetail(long p_PJP_MASTER_ID, int p_AREA_ID, int p_DATE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspInsertPJPDetail mSlashCodes = new uspInsertPJPDetail();
                mSlashCodes.Connection = mConnection;
                mSlashCodes.PJP_MASTER_ID = p_PJP_MASTER_ID;
                mSlashCodes.AREA_ID = p_AREA_ID;
                mSlashCodes.DATE_ID = p_DATE_ID;
                bool chk = mSlashCodes.ExecuteQuery();
                int ret = 0;
                if (chk)
                {
                    ret = 1;
                }
                return ret;

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return 0;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }


        }


        public DataTable getPJP(int p_Distributor_id, int p_ORDER_BOOKER_ID, int p_DATE_TYPE, DateTime p_EFECTIVE_START_DATE,
          DateTime p_EFFECTIVE_END_DATE, int p_USER_ID, bool p_IS_ACTIVE, int p_AREA_ID
          , int p_DATE_ID, int p_TYPE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spCRUDPJP mSlashCodes = new spCRUDPJP();
                mSlashCodes.Connection = mConnection;
                mSlashCodes.DISTRIBUTOR_ID = p_Distributor_id;
                mSlashCodes.ORDER_BOOKER_ID = p_ORDER_BOOKER_ID;
                mSlashCodes.DATE_TYPE = p_DATE_TYPE;
                mSlashCodes.EFECTIVE_START_DATE = p_EFECTIVE_START_DATE;
                mSlashCodes.TIME_STAMP = DateTime.Now;
                mSlashCodes.MODIFIED_DATE = DateTime.Now;
                mSlashCodes.EFFECTIVE_END_DATE = p_EFFECTIVE_END_DATE;
                mSlashCodes.USER_ID = p_USER_ID;
                mSlashCodes.IS_ACTIVE = p_IS_ACTIVE;
                mSlashCodes.TYPE_ID = p_TYPE_ID;
                DataTable dt = new DataTable();

                dt = mSlashCodes.ExecuteTable();
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
        public DataTable SelectSalesForceAreaPJP(int p_Distributor_Id, int p_SaleForce_Id, long p_Area_Id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectSALEFORCE_AREA_ASSIGNMENTnew mArea = new spSelectSALEFORCE_AREA_ASSIGNMENTnew();
                mArea.Connection = mConnection;
                mArea.DISTRIBUTOR_ID = p_Distributor_Id;
                mArea.SALEFORCE_ID = p_SaleForce_Id;
                mArea.AREA_ID = p_Area_Id;
                DataTable dt = mArea.ExecuteTable();
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
        public DataTable SelectLogo(int Ptype_id, int p_user_id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spINSERTlOGO mSlashCodes = new spINSERTlOGO();
                mSlashCodes.Connection = mConnection;
                mSlashCodes.Connection = mConnection;
                mSlashCodes.CREATE_DATE = DateTime.Now;
                mSlashCodes.UPDATE_DATE = DateTime.Now;
                mSlashCodes.LOGO_NAME = "";
                mSlashCodes.USER_ID = p_user_id;
                mSlashCodes.type_id = Ptype_id;
                DataTable dt = mSlashCodes.ExecuteTable();
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
        public string UploadLogo(int p_user_id, string p_ImagePath, DateTime p_Time_Stamp, int p_type_id)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spINSERTlOGO mSlashCodes = new spINSERTlOGO();
                mSlashCodes.Connection = mConnection;
                mSlashCodes.CREATE_DATE = DateTime.Now;
                mSlashCodes.UPDATE_DATE = DateTime.Now;
                mSlashCodes.LOGO_NAME = p_ImagePath;
                mSlashCodes.USER_ID = p_user_id;
                mSlashCodes.type_id = p_type_id;

                mSlashCodes.IS_ACTIVE = 1;

                mSlashCodes.ExecuteQuery();

                return "Record Updated";
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }
        public string UpdateLogo(int p_logoId, int p_TypeId, DateTime p_Time_Stamp)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spINSERTlOGO mSlashCodes = new spINSERTlOGO();
                mSlashCodes.Connection = mConnection;
                mSlashCodes.Connection = mConnection;
                mSlashCodes.CREATE_DATE = DateTime.Now;
                mSlashCodes.UPDATE_DATE = DateTime.Now;
                mSlashCodes.LOGO_NAME = "";
                mSlashCodes.USER_ID = p_logoId;
                mSlashCodes.type_id = p_TypeId;

                mSlashCodes.ExecuteQuery();

                return "Record Updated";
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return exp.Message;
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

        #endregion
    }
}
