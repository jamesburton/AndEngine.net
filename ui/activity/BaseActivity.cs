namespace andengine.ui.activity
{

	//using Callable = Java.Util.concurrent.Callable;
	//using Callable = andengine.util.Callable;

	//using AsyncCallable = andengine.util.AsyncCallable;
	//using Callback = andengine.util.Callback;
	using Debug = andengine.util.Debug;
	//using ProgressCallable = andengine.util.progress.ProgressCallable;
	using IProgressListener = andengine.util.progress.IProgressListener;

	using Activity = Android.App.Activity;
	using ProgressDialog = Android.App.ProgressDialog;
	using AsyncTask = Android.OS.AsyncTask;

	using Exception = System.Exception;

	using ProgressDialogStyle = Android.App.ProgressDialogStyle;

	/**
	 * @author Nicolas Gramlich
	 * @since 18:35:28 - 29.08.2009
	 */
	public abstract class BaseActivity : Activity
	{
		// ===========================================================
		// Constants
		// ===========================================================

		// ===========================================================
		// Fields
		// ===========================================================

		// ===========================================================
		// Constructors
		// ===========================================================

		// ===========================================================
		// Getter & Setter
		// ===========================================================

		// ===========================================================
		// Methods for/from SuperClass/Interfaces
		// ===========================================================

		// ===========================================================
		// Methods
		// ===========================================================

		/**
		 * Performs a task in the background, showing a {@link ProgressDialog},
		 * while the {@link Callable} is being processed.
		 * 
		 * @param <T>
		 * @param pTitleResID
		 * @param pMessageResID
		 * @param pErrorMessageResID
		 * @param pCallable
		 * @param pCallback
		 */
		protected void DoAsync<T>(int pTitleResID, int pMessageResID, andengine.util.Callable<T> pCallable, andengine.util.Callback<T> pCallback)
		{
			this.DoAsync(pTitleResID, pMessageResID, pCallable, pCallback, null);
		}

		/**
		 * Performs a task in the background, showing a indeterminate {@link ProgressDialog},
		 * while the {@link Callable} is being processed.
		 * 
		 * @param <T>
		 * @param pTitleResID
		 * @param pMessageResID
		 * @param pErrorMessageResID
		 * @param pCallable
		 * @param pCallback
		 * @param pExceptionCallback
		 */
		protected void DoAsync<T>(int pTitleResID, int pMessageResID, andengine.util.Callable<T> pCallable, andengine.util.Callback<T> pCallback, andengine.util.Callback<Exception> pExceptionCallback)
		{
			//new BaseActivityAsyncTask<T>(this, pTitleResID, pMessageResID).Execute((object[])null);
			new BaseActivityAsyncTask<T>(this, pTitleResID, pMessageResID, pCallable, pCallback, pExceptionCallback).DoInBackground((object[])null);
		}

		internal class BaseActivityAsyncTask<T> : AsyncTask
		{
			internal readonly BaseActivity _this;
			internal readonly int pTitleResID;
			internal readonly int pMessageResID;
			internal readonly andengine.util.Callable<T> pCallable;
			internal readonly andengine.util.Callback<T> pCallback;
			internal readonly andengine.util.Callback<Exception> pExceptionCallback;

			public BaseActivityAsyncTask(BaseActivity _this, int pTitleResID, int pMessageResID, andengine.util.Callable<T> pCallable, andengine.util.Callback<T> pCallback, andengine.util.Callback<Exception> pExceptionCallback)
				: base()
			{
				this._this = _this;
				this.pTitleResID = pTitleResID;
				this.pMessageResID = pMessageResID;
				this.pCallable = pCallable;
				this.pCallback = pCallback;
				this.pExceptionCallback = pExceptionCallback;
			}

			private ProgressDialog mPD;
			private Exception mException = null;

			protected override void OnPreExecute()
			{
				this.mPD = ProgressDialog.Show(_this, _this.GetString(pTitleResID), _this.GetString(pMessageResID));
				base.OnPreExecute();
			}

			public virtual T DoInBackground(params object[] parameters)
			{
				try
				{
					return pCallable.Call();
				}
				catch (Exception e)
				{
					this.mException = e;
				}
				//return null;
				return default(T);
			}

			public /* override */ void OnPostExecute(
				//* final 
				T result)
			{
				try
				{
					this.mPD.Dismiss();
				}
				catch (
					//final 
					Exception e)
				{
					Debug.E("Error", e);
				}

				if (this.IsCancelled)
				{
					this.mException = new CancelledException();
				}

				if (this.mException == null)
				{
					pCallback.OnCallback(result);
				}
				else
				{
					if (pExceptionCallback == null)
					{
						Debug.E("Error", this.mException);
					}
					else
					{
						pExceptionCallback.OnCallback(this.mException);
					}
				}

				base.OnPostExecute(result);
			}
		}

		/**
		 * Performs a task in the background, showing a {@link ProgressDialog} with an ProgressBar,
		 * while the {@link AsyncCallable} is being processed.
		 * 
		 * @param <T>
		 * @param pTitleResID
		 * @param pMessageResID
		 * @param pErrorMessageResID
		 * @param pAsyncCallable
		 * @param pCallback
		 */
		protected void DoProgressAsync<T>(int pTitleResID, andengine.util.progress.ProgressCallable<T> pCallable, andengine.util.Callback<T> pCallback)
		{
			this.DoProgressAsync(pTitleResID, pCallable, pCallback, null);
		}

		/**
		 * Performs a task in the background, showing a {@link ProgressDialog} with a ProgressBar,
		 * while the {@link AsyncCallable} is being processed.
		 * 
		 * @param <T>
		 * @param pTitleResID
		 * @param pMessageResID
		 * @param pErrorMessageResID
		 * @param pAsyncCallable
		 * @param pCallback
		 * @param pExceptionCallback
		 */
		/*
		protected void DoProgressAsync<T>(int pTitleResID, ProgressCallable<T> pCallable, Callback<T> pCallback, Callback<Exception> pExceptionCallback) {
			new AsyncTask<Void, Integer, T>() {
				private ProgressDialog mPD;
				private Exception mException = null;

				public override void OnPreExecute() {
					this.mPD = new ProgressDialog(BaseActivity.this);
					this.mPD.SetTitle(pTitleResID);
					this.mPD.SetIcon(Android.Resource.Drawable.ic_menu_save);
					this.mPD.SetIndeterminate(false);
					this.mPD.SetProgressStyle(ProgressDialog.STYLE_HORIZONTAL);
					this.mPD.Show();
					base.OnPreExecute();
				}

				public override T DoInBackground(
					// final Void... params
					params object[] parameters) {
					try {
						return pCallable.call(new IProgressListener() {
							public override void OnProgressChanged(int pProgress) {
								OnProgressUpdate(pProgress);
							}
						});
					} catch (Exception e) {
						this.mException = e;
					}
					return null;
				}

				public override void OnProgressUpdate(
					// final Integer... values
					params int[] values) {
					this.mPD.SetProgress(values[0]);
				}

				public override void OnPostExecute(T result) {
					try {
						this.mPD.Dismiss();
					} catch (Exception e) {
						Debug.E("Error", e);
						//* Nothing.
					}

					if(this.IsCancelled()) {
						this.mException = new CancelledException();
					}

					if(this.mException == null) {
						pCallback.OnCallback(result);
					} else {
						if(pExceptionCallback == null) {
							Debug.E("Error", this.mException);
						} else {
							pExceptionCallback.OnCallback(this.mException);
						}
					}

					base.OnPostExecute(result);
				}
			}.Execute((Void[]) null);
		}
		//*/
		protected void DoProgressAsync<T>(int pTitleResID, andengine.util.progress.ProgressCallable<T> pCallable, andengine.util.Callback<T> pCallback, andengine.util.Callback<Exception> pExceptionCallback)
		{
			//new BaseActivityAsyncTask2<T>(this, pTitleResID, pCallable, pCallback, pExceptionCallback).Execute((object[])null);
			new BaseActivityAsyncTask2<T>(this, pTitleResID, pCallable, pCallback, pExceptionCallback).DoInBackground((object[])null);
		}

		public class BaseActivityAsyncTask2<T> : AsyncTask
		{
			private ProgressDialog mPD;
			private Exception mException = null;

			internal readonly BaseActivity _this;
			internal readonly int pTitleResID;
			internal readonly andengine.util.progress.ProgressCallable<T> pCallable;
			internal readonly andengine.util.Callback<T> pCallback;
			internal readonly andengine.util.Callback<Exception> pExceptionCallback;

			public BaseActivityAsyncTask2(BaseActivity _this, int pTitleResID, andengine.util.progress.ProgressCallable<T> pCallable, andengine.util.Callback<T> pCallback, andengine.util.Callback<Exception> pExceptionCallback)
				: base()
			{
				this._this = _this;
				this.pTitleResID = pTitleResID;
				this.pCallable = pCallable;
				this.pCallback = pCallback;
				this.pExceptionCallback = pExceptionCallback;
			}

			protected override void OnPreExecute()
			{
				this.mPD = new ProgressDialog(_this);
				this.mPD.SetTitle(pTitleResID);
				this.mPD.SetIcon(Android.Resource.Drawable.IcMenuSave);
				this.mPD.Indeterminate = false;
				this.mPD.SetProgressStyle(ProgressDialogStyle.Horizontal);
				this.mPD.Show();
				base.OnPreExecute();
			}

			public /* override */ T DoInBackground(
				// final Void... params
				params object[] parameters)
			{
				try
				{
					return pCallable.Call(new BackgroundProgressListener(this));
				}
				catch (Exception e)
				{
					this.mException = e;
				}
				//return null;
				return default(T);
			}

			public class BackgroundProgressListener : IProgressListener
			{
				internal BaseActivityAsyncTask2<T> _this;
				public BackgroundProgressListener(BaseActivityAsyncTask2<T> _this)
				{
					this._this = _this;
				}
				public /* override */ void OnProgressChanged(int pProgress)
				{
					_this.OnProgressUpdate(pProgress);
				}
			}

			public /* override */ void OnProgressUpdate(
				// final Integer... values
				params int[] values)
			{
				this.mPD.Progress = values[0];
			}

			public /* override */ void OnPostExecute(T result)
			{
				try
				{
					this.mPD.Dismiss();
				}
				catch (Exception e)
				{
					Debug.E("Error", e);
					//* Nothing.
				}

				if (this.IsCancelled)
				{
					this.mException = new CancelledException();
				}

				if (this.mException == null)
				{
					pCallback.OnCallback(result);
				}
				else
				{
					if (pExceptionCallback == null)
					{
						Debug.E("Error", this.mException);
					}
					else
					{
						pExceptionCallback.OnCallback(this.mException);
					}
				}

				base.OnPostExecute(result);
			}
		}

		/**
		 * Performs a task in the background, showing an indeterminate {@link ProgressDialog},
		 * while the {@link AsyncCallable} is being processed.
		 * 
		 * @param <T>
		 * @param pTitleResID
		 * @param pMessageResID
		 * @param pErrorMessageResID
		 * @param pAsyncCallable
		 * @param pCallback
		 * @param pExceptionCallback
		 */
		protected void DoAsync<T>(int pTitleResID, int pMessageResID, andengine.util.AsyncCallable<T> pAsyncCallable, andengine.util.Callback<T> pCallback, andengine.util.Callback<Exception> pExceptionCallback) {
		ProgressDialog pd = ProgressDialog.Show(this, this.GetString(pTitleResID), this.GetString(pMessageResID));
		pAsyncCallable.Call(new DoAsync_Callback<T>(this, ref pd, ref pCallback), pExceptionCallback);
	}
		internal class DoAsync_Callback<T>
			: andengine.util.Callback<T>
		{
			internal readonly BaseActivity _this;
			internal readonly ProgressDialog pd;
			internal readonly andengine.util.Callback<T> pCallback;
			public DoAsync_Callback(BaseActivity _this, ref ProgressDialog pd, ref andengine.util.Callback<T> pCallback)
			{
				this._this = _this;
				this.pd = pd;
				this.pCallback = pCallback;
			}

			public /* interface, so not need to: override */ void OnCallback(T result)
			{
				try
				{
					pd.Dismiss();
				}
				catch (Exception e)
				{
					Debug.E("Error", e);
					/* Nothing. */
				}

				pCallback.OnCallback(result);
			}
		}

		// ===========================================================
		// Inner and Anonymous Classes
		// ===========================================================

		public /* static */ class CancelledException : Exception
		{
			//private static final long serialVersionUID = -78123211381435596L;
			private static long serialVersionUID = -78123211381435596L;
		}
	}
}