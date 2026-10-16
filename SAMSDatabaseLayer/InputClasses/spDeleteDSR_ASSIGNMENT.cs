using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;

namespace SAMSDatabaseLayer.Classes
{
	public class spDeleteDSR_ASSIGNMENT
	{
		#region Private Members
		private string sp_Name = "spDeleteDSR_ASSIGNMENT" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private int m_DSR_ASSIGNMENT_ID;
		private int m_NSM_ID;
		private int m_RSM_ID;
		private int m_ASM_ASO_ID;
		private int m_TSO_TSE_ID;
		private int m_DISTRIBUTOR_ID;
		private int m_ASSIGNED_DSR_ID;
		#endregion
		#region Public Properties
		public int DSR_ASSIGNMENT_ID
		{
			set
			{
				m_DSR_ASSIGNMENT_ID = value ;
			}
			get
			{
				return m_DSR_ASSIGNMENT_ID;
			}
		}
		public int NSM_ID
		{
			set
			{
				m_NSM_ID = value ;
			}
			get
			{
				return m_NSM_ID;
			}
		}
		public int RSM_ID
		{
			set
			{
				m_RSM_ID = value ;
			}
			get
			{
				return m_RSM_ID;
			}
		}
		public int ASM_ASO_ID
		{
			set
			{
				m_ASM_ASO_ID = value ;
			}
			get
			{
				return m_ASM_ASO_ID;
			}
		}
		public int TSO_TSE_ID
		{
			set
			{
				m_TSO_TSE_ID = value ;
			}
			get
			{
				return m_TSO_TSE_ID;
			}
		}
		public int DISTRIBUTOR_ID
		{
			set
			{
				m_DISTRIBUTOR_ID = value ;
			}
			get
			{
				return m_DISTRIBUTOR_ID;
			}
		}
		public int ASSIGNED_DSR_ID
		{
			set
			{
				m_ASSIGNED_DSR_ID = value ;
			}
			get
			{
				return m_ASSIGNED_DSR_ID;
			}
		}


		public IDbConnection  Connection
		{
			set
			{
				m_connection = value;
			}
			get
			{
				return m_connection;
			}
		}
		public IDbTransaction  Transaction
		{
			set
			{
				m_transaction = value;
			}
			get
			{
				return m_transaction;
			}
		}
		#endregion
		#region Constructor
		public spDeleteDSR_ASSIGNMENT()
		{
            m_ASSIGNED_DSR_ID = Constants.IntNullValue;
            m_DSR_ASSIGNMENT_ID = Constants.IntNullValue;
            m_NSM_ID = Constants.IntNullValue;
            m_RSM_ID = Constants.IntNullValue;
            m_ASM_ASO_ID = Constants.IntNullValue;
            m_TSO_TSE_ID = Constants.IntNullValue;
            m_DISTRIBUTOR_ID = Constants.IntNullValue;
            m_ASSIGNED_DSR_ID = Constants.IntNullValue;

        }
		#endregion
		#region public Methods
		public bool  ExecuteQuery()
		{
			try
			{
			    IDbCommand cmd = ProviderFactory.GetCommand(EnumProviders.SQLClient);
				cmd.CommandType =  CommandType.StoredProcedure;
				cmd.CommandText = sp_Name;
				cmd.Connection =   m_connection;
				if(m_transaction!=null)
				{
					cmd.Transaction = m_transaction;
				}
				GetParameterCollection(ref cmd);
				cmd.ExecuteNonQuery();
				return true;
			}
			catch(Exception e)
			{
				throw e;
			}
			finally
			{
			}
		}
		public IDataReader ExecuteReader()
		{
			try
			{
				IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
				command.CommandType = CommandType.StoredProcedure;
				command.CommandText = sp_Name;
				command.Connection = m_connection;
				if(m_transaction!=null)
				{
					command.Transaction = m_transaction;
				}
				GetParameterCollection(ref command);
				IDataReader dr = command.ExecuteReader();
				return dr;
			}
			catch(Exception exp)
			{
				throw exp;
			}
			finally
			{
			}
		}
		public DataTable ExecuteTable()
		{
			try
			{
				IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
				command.CommandType = CommandType.StoredProcedure;
				command.CommandText = sp_Name;
				command.Connection = m_connection;
				if(m_transaction!=null)
				{
					command.Transaction = m_transaction;
				}
				GetParameterCollection(ref command);
				IDbDataAdapter da = ProviderFactory.GetAdapter(EnumProviders.SQLClient);
				da.SelectCommand = command;
				DataSet ds = new DataSet();
				da.Fill(ds);
				return ds.Tables[0];
			}
			catch(Exception exp)
			{
				throw exp;
			}
			finally
			{
			}
		}
		public string ExecuteScalar()
		{
			try
			{
				IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
				command.CommandType = CommandType.StoredProcedure;
				command.CommandText = sp_Name;
				command.Connection = m_connection;
				if(m_transaction!=null)
				{
					command.Transaction = m_transaction;
				}
				GetParameterCollection(ref command);
				object o;
				o = command.ExecuteScalar();


				return o.ToString();
			}
			catch(Exception exp)
			{
				throw exp;
			}
			finally
			{
			}
		}
		public void FirstReader(IDataReader dr)
		{
			if(dr.Read())
			{
				m_DSR_ASSIGNMENT_ID= Convert.ToInt32(dr["DSR_ASSIGNMENT_ID"]);
				m_NSM_ID= Convert.ToInt32(dr["NSM_ID"]);
				m_RSM_ID= Convert.ToInt32(dr["RSM_ID"]);
				m_ASM_ASO_ID= Convert.ToInt32(dr["ASM_ASO_ID"]);
				m_TSO_TSE_ID= Convert.ToInt32(dr["TSO_TSE_ID"]);
				m_DISTRIBUTOR_ID= Convert.ToInt32(dr["DISTRIBUTOR_ID"]);
				m_ASSIGNED_DSR_ID= Convert.ToInt32(dr["ASSIGNED_DSR_ID"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@DSR_ASSIGNMENT_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_DSR_ASSIGNMENT_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_DSR_ASSIGNMENT_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@NSM_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_NSM_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_NSM_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@RSM_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_RSM_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_RSM_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@ASM_ASO_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_ASM_ASO_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_ASM_ASO_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@TSO_TSE_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_TSO_TSE_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_TSO_TSE_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@DISTRIBUTOR_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_DISTRIBUTOR_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_DISTRIBUTOR_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@ASSIGNED_DSR_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
			if(m_ASSIGNED_DSR_ID==Constants.IntNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_ASSIGNED_DSR_ID;
			}
			pparams.Add(parameter);


		}
		#endregion
	}
}
