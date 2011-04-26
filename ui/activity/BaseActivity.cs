namespace andengine.ui.activity
{
/*
import java.util.concurrent.Callable;

import org.anddev.andengine.util.AsyncCallable;
import org.anddev.andengine.util.Callback;
import org.anddev.andengine.util.Debug;
import org.anddev.andengine.util.ProgressCallable;
import org.anddev.progressmonitor.IProgressListener;

import android.app.Activity;
import android.app.ProgressDialog;
import android.os.AsyncTask;
*/

     //using Callable = Java.Util.concurrent.Callable;
    //using Callable = andengine.util.Callable;

    //using AsyncCallable = andengine.util.AsyncCallable;
    //using Callback = andengine.util.Callback;
    using Debug = andengine.util.Debug;
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
public abstract class BaseActivity : Activity {
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
	protected void DoAsync<T>(int pTitleResID, int pMessageResID, andengine.util.Callable<T> pCallable, andengine.util.Callback<T> pCallback) {
		this.DoAsync<T>(pTitleResID, pMessageResID, pCallable, pCallback, null);
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
	protected void DoAsync<T>(int pTitleResID, int pMessageResID, andengine.util.Callable<T> pCallable, andengine.util.Callback<T> pCallback, andengine.util.Callback<Exception> pExceptionCallback) {
        /*
		new AsyncTask<Void, Void, T>() {
			private ProgressDialog mPD;
			private Exception mException = null;

			@Override
			public void onPreExecute() {
				this.mPD = ProgressDialog.show(BaseActivity.this, BaseActivity.this.getString(pTitleResID), BaseActivity.this.getString(pMessageResID));
				super.onPreExecute();
			}

			@Override
			public T doInBackground(final Void... params) {
				try {
					return pCallable.call();
				} catch (final Exception e) {
					this.mException = e;
				}
				return null;
			}

			@Override
			public void onPostExecute(final T result) {
				try {
					this.mPD.dismiss();
				} catch (final Exception e) {
					Debug.e("Error", e);
				}

				if(this.isCancelled()) {
					this.mException = new CancelledException();
				}

				if(this.mException == null) {
					pCallback.onCallback(result);
				} else {
					if(pExceptionCallback == null) {
						Debug.e("Error", this.mException);
					} else {
						pExceptionCallback.onCallback(this.mException);
					}
				}

				super.onPostExecute(result);
			}
		}.execute((Void[]) null);
        */ // NB: In-method class moved just immediately below (DoAsync_AsyncTask1)
        new DoAsync_AsyncTask<T>(this, pTitleResID, pMessageResID, pCallable, pCallback, pExceptionCallback).Execute((object[]) null);
	}

    public class DoAsync_AsyncTask<T> : Android.OS.AsyncTask<object, object, T> {
        internal BaseActivity _this;
        internal int pTitleResID;
        internal int pMessageResID;
        internal andengine.util.Callable<T> pCallable;
        internal andengine.util.Callback<T> pCallback;
        internal andengine.util.Callback<Exception> pExceptionCallback;

		private ProgressDialog mPD;
		private Exception mException = null;

        //public DoAsync_AsyncTask1(BaseActivity _this) { this._this = _this; }
        public DoAsync_AsyncTask(BaseActivity _this, int pTitleResID, int pMessageResID, andengine.util.Callable<T> pCallable, andengine.util.Callback<T> pCallback, andengine.util.Callback<Exception> pExceptionCallback)
        { this._this = _this; this.pTitleResID = pTitleResID; this.pMessageResID = pMessageResID; this.pCallable = pCallable; this.pCallback = pCallback; this.pExceptionCallback = pExceptionCallback; }

		//@Override
		public void OnPreExecute() {
			//this.mPD = ProgressDialog.Show(BaseActivity.this, BaseActivity.this.getString(pTitleResID), BaseActivity.this.getString(pMessageResID));
			this.mPD = ProgressDialog.Show(_this, _this.GetString(pTitleResID), _this.GetString(pMessageResID));
			base.OnPreExecute();
		}

		//@Override
		public T DoInBackground(object[] @params) {
			try {
				return pCallable.Call();
			} catch (Exception e) {
				this.mException = e;
			}
			//return null;
            return default(T);
		}

		//@Override
		public void OnPostExecute(T result) {
			try {
				this.mPD.Dismiss();
			} catch (Exception e) {
				Debug.E("Error", e);
			}

			if(this.IsCancelled) {
				this.mException = new CancelledException(); // NB: Didn't exist, so added below
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
	protected void DoProgressAsync<T>(int pTitleResID, andengine.util.progress.ProgressCallable<T> pCallable, andengine.util.Callback<T> pCallback) {
		this.DoProgressAsync<T>(pTitleResID, pCallable, pCallback, null);
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
	protected void DoProgressAsync<T>(int pTitleResID, andengine.util.progress.ProgressCallable<T> pCallable, andengine.util.Callback<T> pCallback, andengine.util.Callback<Exception> pExceptionCallback) {
        /* Moved into new inner class: DoProssAsync_AsyncTask
		new AsyncTask<Void, Integer, T>() {
		    private ProgressDialog mPD;
		    private Exception mException = null;

		    @Override
		    public void onPreExecute() {
			    this.mPD = new ProgressDialog(BaseActivity.this);
			    this.mPD.setTitle(pTitleResID);
			    this.mPD.setIcon(android.R.drawable.ic_menu_save);
			    this.mPD.setIndeterminate(false);
			    this.mPD.setProgressStyle(ProgressDialog.STYLE_HORIZONTAL);
			    this.mPD.show();
			    super.onPreExecute();
		    }

		    @Override
		    public T doInBackground(final Void... params) {
			    try {
				    return pCallable.call(new IProgressListener() {
					    @Override
					    public void onProgressChanged(final int pProgress) {
						    onProgressUpdate(pProgress);
					    }
				    });
			    } catch (final Exception e) {
				    this.mException = e;
			    }
			    return null;
		    }

		    @Override
		    public void onProgressUpdate(final Integer... values) {
			    this.mPD.setProgress(values[0]);
		    }

		    @Override
		    public void onPostExecute(final T result) {
			    try {
				    this.mPD.dismiss();
			    } catch (final Exception e) {
				    Debug.e("Error", e);
				    /* Nothing. * /
			    }

			    if(this.isCancelled()) {
				    this.mException = new CancelledException();
			    }

			    if(this.mException == null) {
				    pCallback.onCallback(result);
			    } else {
				    if(pExceptionCallback == null) {
					    Debug.e("Error", this.mException);
				    } else {
					    pExceptionCallback.onCallback(this.mException);
				    }
			    }

			    super.onPostExecute(result);
		    }
		}.execute((Void[]) null);
        */
        new DoProgressAsync_AsyncTask<T>(this, pTitleResID, pCallable, pCallback, pExceptionCallback).Execute();
	}

    public class DoProgressAsync_AsyncTask<T> : Android.OS.AsyncTask<object, int, T> {
        internal BaseActivity _this;
        internal int pTitleResID;
        internal andengine.util.progress.ProgressCallable<T> pCallable;
        internal andengine.util.Callback<T> pCallback;
        internal andengine.util.Callback<Exception> pExceptionCallback;

		private ProgressDialog mPD;
		private Exception mException = null;

        public DoProgressAsync_AsyncTask(BaseActivity _this, int pTitleResID, andengine.util.progress.ProgressCallable<T> pCallable,
            andengine.util.Callback<T> pCallback, andengine.util.Callback<Exception> pExceptionCallback) {
            this._this = _this;
            this.pTitleResID = pTitleResID;
            this.pCallable = pCallable;
            this.pCallback = pCallback;
            this.pExceptionCallback = pExceptionCallback;
        }

		//@Override
		public new void OnPreExecute() {
			this.mPD = new ProgressDialog(_this);
			this.mPD.SetTitle(pTitleResID);
			this.mPD.SetIcon(Android.Resource.Drawable.IcMenuSave);
			//this.mPD.SetIndeterminateDrawable(false);
            this.mPD.Indeterminate = false;
			this.mPD.SetProgressStyle(ProgressDialogStyle.Horizontal);
			this.mPD.Show();
			base.OnPreExecute();
		}

		//@Override
		public T DoInBackground(object[] @params) {
			try {
                /*
				return pCallable.call(new IProgressListener() {
					//@Override
					public override void onProgressChanged(final int pProgress) {
						onProgressUpdate(pProgress);
					}
				});
                */
                return pCallable.Call((IProgressListener)new DoInBackground_ProgressListener(this));
			} catch (Exception e) {
				this.mException = e;
			}
			//return null;
            return default(T);
		}

        internal class DoInBackground_ProgressListener : IProgressListener {
            internal DoAsync_AsyncTask<T> _this;
            internal DoInBackground_ProgressListener(DoAsync_AsyncTask<T> _this) { this._this = _this; }
            public void OnProgressChanged(int pProgress) { _this.OnProgressUpdate(pProgress); }
        }

		//@Override
		public void OnProgressUpdate(object[] values) {
			//this.mPD.setProgress(values[0]);
            this.mPD.Progress = (int)values[0];
		}

		//@Override
		public void OnPostExecute(T result) {
			try {
				this.mPD.Dismiss();
			} catch (Exception e) {
				Debug.E("Error", e);
				/* Nothing. */
			}

			if(this.IsCancelled) {
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
        /*
		pAsyncCallable.Call(new Callback<T>() {
			@Override
			public void onCallback(final T result) {
				try {
					pd.dismiss();
				} catch (final Exception e) {
					Debug.e("Error", e);
					/* Nothing. * /
				}

				pCallback.onCallback(result);
			}
		}, pExceptionCallback);
        */
        pAsyncCallable.Call(new DoAsync_Callback<T>(this, pd, pCallback), pExceptionCallback);
	}

    public class DoAsync_Callback<T> : andengine.util.Callback<T> 
    {
        internal BaseActivity _this;
        internal ProgressDialog pd;
        internal andengine.util.Callback<T> pCallback;
        public DoAsync_Callback(BaseActivity _this, ProgressDialog pd, andengine.util.Callback<T> pCallback) { this._this = _this; this.pCallback = pCallback; }
        public void OnCallback(T result) {
            try {
                pd.Dismiss();
            } catch(Exception e) {
                Debug.E("Error", e);
            }
        }
    }

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================

	public /* static */ class CancelledException : Exception {
		private static long serialVersionUID = -78123211381435596L;
	}
}
}