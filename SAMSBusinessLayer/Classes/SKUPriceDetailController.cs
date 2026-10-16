using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;

namespace SAMSBusinessLayer.Classes
{
    /// <summary>
    /// Class For SKU Price Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert SKU Prices
    /// </item>
    /// <item>
    /// Get SKU Prices
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
   public class SKUPriceDetailController
   {
       #region Constructors

        /// <summary>
       /// Constructor For SKUPriceDetailController
        /// </summary>
       public SKUPriceDetailController()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
        #endregion

       #region Public Methods

       #region Select

       /// <summary>
       /// Gets SKU Price Data
       /// </summary>
       /// <remarks>
        /// Returns SKU Price Data as Datatable
       /// </remarks>
       /// <param name="p_Principal_Id">Principal</param>
       /// <param name="p_Division_ID">Division</param>
       /// <param name="p_Category_ID">Category</param>
       /// <param name="p_Brand_id">Brand</param>
       /// <param name="p_Var_Id">Variant</param>
       /// <param name="p_User_Id">InsertedBy</param>
       /// <param name="p_sku_id">SKU</param>
       /// <param name="p_Type_id">Type</param>
       /// <param name="p_ClosedDate">CloseDate</param>
        /// <returns>SKU Price Data as Datatable</returns>
       public DataTable SelectDataPrice(int p_Principal_Id, int p_Division_ID, int p_Category_ID, int p_Brand_id, int p_Var_Id, int p_User_Id,int p_sku_id,int p_Type_id,DateTime p_ClosedDate)
       {
           IDbConnection mConnection = null;
           try
           {
               mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
               mConnection.Open();

               UspSelectSKUPriceList  ObjSelect = new UspSelectSKUPriceList();
               ObjSelect.Connection = mConnection;
               ObjSelect.COMPANY_ID = p_Principal_Id;
               ObjSelect.DIVISION_ID = p_Division_ID;
               ObjSelect.CATEGORY_ID = p_Category_ID;
               ObjSelect.BRAND_ID = p_Brand_id;
               ObjSelect.VAR_ID = p_Var_Id;
               ObjSelect.SKU_ID = p_sku_id;
               ObjSelect.TYPEID = p_Type_id;
               ObjSelect.USER_ID = p_User_Id;
               ObjSelect.DAYCLOSED = p_ClosedDate; 
               DataTable dt = ObjSelect.ExecuteTable();
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

        public DataTable SelectDistributorSharePrincipal(int p_Principal_Id, int p_Distributor_ID, int p_User_Id, int p_Type_id, DateTime p_ClosedDate)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                UspSelectSKUPriceList ObjSelect = new UspSelectSKUPriceList();
                ObjSelect.Connection = mConnection;
                ObjSelect.COMPANY_ID = p_Principal_Id;
                ObjSelect.DIVISION_ID = Constants.IntNullValue;
                ObjSelect.CATEGORY_ID = Constants.IntNullValue;
                ObjSelect.BRAND_ID = Constants.IntNullValue;
                ObjSelect.VAR_ID = p_Distributor_ID;
                ObjSelect.SKU_ID = Constants.IntNullValue;
                ObjSelect.TYPEID = p_Type_id;
                ObjSelect.USER_ID = p_User_Id;
                ObjSelect.DAYCLOSED = p_ClosedDate;
                DataTable dt = ObjSelect.ExecuteTable();
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

        /// <summary>
        /// Gets SKU Current Price Data
        /// </summary>
        /// <remarks>
        /// Returns SKU Current Price Data as Datatable
        /// </remarks>
        /// <param name="p_Uom_id">UOM</param>
        /// <param name="p_Company_id">Principal</param>
        /// <param name="p_Division_Id">Division</param>
        /// <param name="p_Brand_Id">Brand</param>
        /// <param name="p_Category_Id">Category</param>
        /// <param name="variant_Id">Variant</param>
        /// <param name="p_Iscurrent">IsCurrent</param>
        /// <param name="p_sku_id">SKU</param>
        /// <returns>SKU Current Price Data as Datatable</returns>
        public DataTable SelectSKuCurrentPrice(int p_Uom_id, int p_Company_id, int p_Division_Id, int p_Brand_Id, int p_Category_Id, int variant_Id, int p_Iscurrent, int p_sku_id)
       {
           IDbConnection mConnection = null;
           try
           {
               mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
               mConnection.Open();
               spSelectskuPriceInfo mSkuInfo = new spSelectskuPriceInfo();
               mSkuInfo.Connection = mConnection;

               mSkuInfo.UOM_ID = p_Uom_id;
               mSkuInfo.COMPANY_ID = p_Company_id;
               mSkuInfo.DIVISION_ID = p_Division_Id;
               mSkuInfo.BRAND_ID = p_Brand_Id;
               mSkuInfo.CATEGORY_ID = p_Category_Id;
               mSkuInfo.VARIANT_ID = variant_Id;
               mSkuInfo.ISACTIVE = true;
               mSkuInfo.ISCURRENT = p_Iscurrent;
               mSkuInfo.SKU_Id = p_sku_id;

               DataTable dt = mSkuInfo.ExecuteTable();
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

        public DataTable GetAllCatgory()
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                uspGetCategories ObjSelect = new uspGetCategories();
                ObjSelect.Connection = mConnection;
                DataTable dt = ObjSelect.ExecuteTable();
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

        #region Insert

        /// <summary>
        /// Insert SKU Price
        /// </summary>
        /// <remarks>
        /// Returns "Record Inserted" On Success And Null On Failure
        /// </remarks>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Sku_Id">SKU</param>
        /// <param name="p_Uom_Id">UOM</param>
        /// <param name="p_Distributor_Discount">Discount</param>
        /// <param name="p_Tax_Price">Tax</param>
        /// <param name="p_Distributor_Price">DP</param>
        /// <param name="p_Trade_Price">TP</param>
        /// <param name="p_Retail_Price">RP</param>
        /// <param name="p_date_effected">DateFrom</param>
        /// <param name="p_SED_Tax">SED</param>
        /// <returns>"Record Inserted" On Success And Null On Failure</returns>
        public string InsertSKU_PRICES(int p_Distributor_Id, int p_Sku_Id, int p_Uom_Id, decimal p_Distributor_Discount, decimal p_Tax_Price, decimal p_Distributor_Price, decimal p_Trade_Price, decimal p_Retail_Price, DateTime p_date_effected, decimal p_SED_Tax,decimal p_SKU_TAX)
       {
           IDbConnection mConnection = null;
           try
           {
               mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
               mConnection.Open();
               UspProcessSKU_Prices mSkuPrices = new UspProcessSKU_Prices();
               mSkuPrices.Connection = mConnection;
               mSkuPrices.DISTRIBUTOR_ID = p_Distributor_Id;
               mSkuPrices.DISTRIBUTOR_PRICE = p_Distributor_Price;
               mSkuPrices.SKU_ID = p_Sku_Id;
               mSkuPrices.RETAIL_PRICE = p_Retail_Price;
               mSkuPrices.TAX_PRICE = p_Tax_Price;
               mSkuPrices.TRADE_PRICE = p_Trade_Price;
               mSkuPrices.DISTRIBUTOR_DISCOUNT = p_Distributor_Discount;
               mSkuPrices.TIME_STAMP = System.DateTime.Now;
               mSkuPrices.LASTUPDATE_DATE = System.DateTime.Now;
               mSkuPrices.DATE_EFFECTED = p_date_effected;
               mSkuPrices.SED_TAX = p_SED_Tax;
                mSkuPrices.SKU_GST = p_SKU_TAX;
               mSkuPrices.ExecuteQuery();
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
       /// Inserts Or Updates SKU Price From Excel File
       /// </summary>
       /// Returns True On Success And False On Failure
       /// <param name="p_DistributorId">Location</param>
       /// <param name="pFileName">ExcelFile</param>
       /// <param name="p_Principal_Id">Principal</param>
       /// <returns>True On Success And False On Failure</returns>
       public bool ImportSKUPrices(int p_DistributorId, string pFileName, int p_Principal_Id)
       {
           IDbConnection mConnection = null;
           FileStream Sourcefile = null;
           StreamReader ReadSourceFile = null;
           IDbTransaction mTransaction = null;
           try
           {
               mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
               mConnection.Open();
               mTransaction = ProviderFactory.GetTransaction(mConnection);

               Sourcefile = new FileStream(pFileName, FileMode.Open);
               ReadSourceFile = new StreamReader(Sourcefile);
               string FileContents = "";
               while ((FileContents = ReadSourceFile.ReadLine()) != null)
               {

                   string[] ParametersArr = FileContents.Split(Constants.File_Delimiter);
                   spSelectSKUS mSKUS = new spSelectSKUS();
                   mSKUS.Connection = mConnection;
                   mSKUS.Transaction = mTransaction;
                   mSKUS.SKU_CODE = ParametersArr[0].ToString();
                   mSKUS.ISACTIVE = true;
                   DataTable dt = mSKUS.ExecuteTable();
                   if (dt.Rows.Count > 0)
                   {
                       UspProcessSKU_Prices mSKUPrice = new UspProcessSKU_Prices();
                       mSKUPrice.Connection = mConnection;
                       mSKUPrice.Transaction = mTransaction;
                       mSKUPrice.SKU_ID = int.Parse(dt.Rows[0]["SKU_Id"].ToString());
                       mSKUPrice.DISTRIBUTOR_ID = p_DistributorId;
                       mSKUPrice.DISTRIBUTOR_PRICE = decimal.Parse(ParametersArr[1].ToString());
                       mSKUPrice.TRADE_PRICE = decimal.Parse(ParametersArr[2].ToString());
                       mSKUPrice.RETAIL_PRICE = decimal.Parse(ParametersArr[3].ToString());
                       mSKUPrice.DISTRIBUTOR_DISCOUNT = decimal.Parse(ParametersArr[4].ToString());
                       mSKUPrice.TAX_PRICE = decimal.Parse(ParametersArr[5].ToString());
                       mSKUPrice.SED_TAX = decimal.Parse(ParametersArr[6].ToString());
                       mSKUPrice.DATE_EFFECTED = DateTime.Parse(ParametersArr[7].ToString());
                       mSKUPrice.LASTUPDATE_DATE = DateTime.Now;
                       mSKUPrice.TIME_STAMP = DateTime.Now;
                        mSKUPrice.SKU_GST= decimal.Parse(ParametersArr[8].ToString());
                        mSKUPrice.ExecuteQuery();
                   }

               }
               mTransaction.Commit();
               return true;
           }

           catch (Exception excp)
           {
               mTransaction.Rollback();
               ReadSourceFile.Close();
               mConnection.Close();
               ExceptionPublisher.PublishException(excp);
               return false;
           }
           finally
           {
               ReadSourceFile.Close();
               mConnection.Close();
           }
       }

        /// <summary>
        /// Inserts Opening Credit From Excel File
        /// </summary>
        /// Returns True On Success And False On Failure
        /// <param name="p_DistributorId">Location</param>
        /// <param name="pFileName">ExcelFile</param>
        /// <param name="p_Principal_Id">Principal</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool ImportOpeningCredit(int p_DistributorId, string pFileName, int p_Principal_Id,DataTable dtCustomer, int p_UserID)
        {
            IDbConnection mConnection = null;
            FileStream Sourcefile = null;
            StreamReader ReadSourceFile = null;
            IDbTransaction mTransaction = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                Sourcefile = new FileStream(pFileName, FileMode.Open);
                ReadSourceFile = new StreamReader(Sourcefile);
                string FileContents = "";

                if (dtCustomer.Rows.Count > 0)
                {
                    while ((FileContents = ReadSourceFile.ReadLine()) != null)
                    {
                        string[] ParametersArr = FileContents.Split(Constants.File_Delimiter);

                        DataRow[] foundRows = dtCustomer.Select("CUSTOMER_ID  = '" + ParametersArr[2].ToString() + "'");
                        if (foundRows.Length > 0)
                        {
                            spInsertSALE_INVOICE_MASTER mISom = new spInsertSALE_INVOICE_MASTER();
                            mISom.Connection = mConnection;
                            mISom.Transaction = mTransaction;

                            mISom.DISTRIBUTOR_ID = p_DistributorId;
                            mISom.MANUAL_INVOICE_ID = ParametersArr[4].ToString();
                            mISom.PRINCIPAL_ID = p_Principal_Id;
                            mISom.AREA_ID = Convert.ToInt32(DataControl.chkNull_Zero(foundRows[0]["AREA_ID"].ToString()));
                            mISom.DELIVERYMAN_ID = Convert.ToInt32(ParametersArr[1]);
                            mISom.ORDERBOOKER_ID = -1;
                            mISom.DOCUMENT_DATE = Convert.ToDateTime(ParametersArr[3]);
                            mISom.SOLD_TO = Convert.ToInt64(ParametersArr[2]);
                            mISom.TOTAL_NET_AMOUNT = Convert.ToDecimal(ParametersArr[5]);
                            mISom.CREDIT_AMOUNT = Convert.ToDecimal(ParametersArr[5]);
                            mISom.CURRENT_CREDIT_AMOUNT = Convert.ToDecimal(ParametersArr[5]);
                            mISom.TOWN_ID = Convert.ToInt32(DataControl.chkNull_Zero(foundRows[0]["TOWN_ID"].ToString()));
                            mISom.USER_ID = p_UserID;
                            mISom.SALE_ORDER_ID = -1;
                            mISom.TIME_STAMP = DateTime.Now;
                            mISom.LASTUPDATE_DATE = DateTime.Now;
                            mISom.IS_DELETED = false;
                            mISom.LEGEND_ID = Convert.ToInt32(ParametersArr[0]);
                            mISom.ExecuteQuery();

                            #region Account Posting

                            LedgerController LController = new LedgerController();
                            Configuration.GetAccountHead();
                            string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DistributorId);
                            LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.AccountReceivable), p_DistributorId, Convert.ToDecimal(ParametersArr[5]), 0, mISom.DOCUMENT_DATE, "CASH Sale Default", DateTime.Now, p_Principal_Id, Convert.ToInt32(ParametersArr[2]), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserID, mTransaction, mConnection, mISom.LEGEND_ID, mISom.DELIVERYMAN_ID);
                            LController.PostingInvoiceAccount(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(Configuration.SaleAccount), p_DistributorId, 0, Convert.ToDecimal(ParametersArr[5]), mISom.DOCUMENT_DATE, "Sale Default", DateTime.Now, p_Principal_Id, Convert.ToInt32(ParametersArr[2]), mISom.SALE_INVOICE_ID, mISom.MANUAL_INVOICE_ID, Constants.Document_SaleInvoice, p_UserID, mTransaction, mConnection, mISom.LEGEND_ID, mISom.DELIVERYMAN_ID);

                            #endregion

                        }
                    }
                    mTransaction.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception excp)
            {
                mTransaction.Rollback();
                ReadSourceFile.Close();
                mConnection.Close();
                ExceptionPublisher.PublishException(excp);
                return false;
            }
            finally
            {
                ReadSourceFile.Close();
                mConnection.Close();
            }
        }
        #endregion


       
        #endregion
    }
}
