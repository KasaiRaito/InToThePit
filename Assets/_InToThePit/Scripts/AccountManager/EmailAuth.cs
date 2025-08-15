using System.Collections;
using UnityEngine;
using TMPro;
using Firebase.Extensions;
using Firebase.Auth;
using Firebase;


public class EmailAuth : MonoBehaviour
{

    [Header("Login")]
    public TMP_InputField LoginEmail;
    public TMP_InputField loginPassword;

    [Header("Sign up")]
    public TMP_InputField SignupEmail;
    public TMP_InputField SignupPassword;
    public TMP_InputField SignupPasswordConfirm;

    /*
    [Header("Extra")]
    public GameObject loadingScreen;
    public TextMeshProUGUI logTxt;
    public GameObject loginUi, signupUi, SuccessUi;
    */

    [Header("Messages")] 
    public GameObject SuccesMessage;
    public GameObject FailureMessage;

    public void SignUp()
    {
        //loadingScreen.SetActive(true);

        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        string email = SignupEmail.text;
        string password = SignupPassword.text;
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                StartCoroutine(ShowMesage(false));
                
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                StartCoroutine(ShowMesage(false));
                return;
            }
            // Firebase user has been created.

            //loadingScreen.SetActive(false);
            AuthResult result = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);

            SignupEmail.text = "";
            SignupPassword.text = "";
            SignupPasswordConfirm.text = "";

            if (result.User.IsEmailVerified)
            {
                Debug.LogWarning("Sign up Successful");
                //showLogMsg("Sign up Successful");
            }
            else
            {
                Debug.LogWarning("Sign up Successful");
                //showLogMsg("Please verify your email!!");
                SendEmailVerification();
            }

        });
    }

    public void SendEmailVerification()
    {
        StartCoroutine(SendEmailForVerificationAsync());
    }

    IEnumerator SendEmailForVerificationAsync()
    {
        FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
        if (user != null)
        {
            var sendEmailTask = user.SendEmailVerificationAsync();
            yield return new WaitUntil(() => sendEmailTask.IsCompleted);

            if (sendEmailTask.Exception != null)
            {
                print("Email send error");
                FirebaseException firebaseException = sendEmailTask.Exception.GetBaseException() as FirebaseException;
                AuthError error = (AuthError)firebaseException.ErrorCode;

                switch (error)
                {
                    case AuthError.None:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.Unimplemented:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.Failure:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.InvalidCustomToken:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.CustomTokenMismatch:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.InvalidCredential:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.UserDisabled:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.AccountExistsWithDifferentCredentials:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.OperationNotAllowed:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.EmailAlreadyInUse:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.RequiresRecentLogin:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.CredentialAlreadyInUse:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.InvalidEmail:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.WrongPassword:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.TooManyRequests:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.UserNotFound:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.ProviderAlreadyLinked:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.NoSuchProvider:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.InvalidUserToken:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.UserTokenExpired:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.NetworkRequestFailed:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.InvalidApiKey:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.AppNotAuthorized:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.UserMismatch:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.WeakPassword:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.NoSignedInUser:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.ApiNotAvailable:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.ExpiredActionCode:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.InvalidActionCode:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.InvalidMessagePayload:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.InvalidPhoneNumber:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.MissingPhoneNumber:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.InvalidRecipientEmail:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.InvalidSender:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.InvalidVerificationCode:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.InvalidVerificationId:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.MissingVerificationCode:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.MissingVerificationId:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.MissingEmail:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.MissingPassword:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.QuotaExceeded:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.RetryPhoneAuth:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.SessionExpired:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.AppNotVerified:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.AppVerificationFailed:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.CaptchaCheckFailed:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.InvalidAppCredential:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.MissingAppCredential:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.InvalidClientId:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.InvalidContinueUri:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.MissingContinueUri:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.KeychainError:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.MissingAppToken:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.MissingIosBundleId:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.NotificationNotForwarded:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.UnauthorizedDomain:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.WebContextAlreadyPresented:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.WebContextCancelled:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.DynamicLinkNotActivated:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.Cancelled:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.InvalidProviderId:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.WebInternalError:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.WebStorateUnsupported:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.TenantIdMismatch:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.UnsupportedTenantOperation:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.InvalidLinkDomain:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.RejectedCredential:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.PhoneNumberNotFound:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.InvalidTenantId:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.MissingClientIdentifier:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.MissingMultiFactorSession:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.MissingMultiFactorInfo:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.InvalidMultiFactorSession:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.MultiFactorInfoNotFound:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.AdminRestrictedOperation:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.UnverifiedEmail:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.SecondFactorAlreadyEnrolled:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.MaximumSecondFactorCountExceeded:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.UnsupportedFirstFactor:
                        StartCoroutine(ShowMesage(false));
                        break;
                    case AuthError.EmailChangeNeedsVerification:
                        StartCoroutine(ShowMesage(false));
                        break;
                    default:
                        StartCoroutine(ShowMesage(false));
                        break;
                }
            }
            else
            {
                print("Email successfully send");
//My Added Line            
                GameplayEventsHUD.onGameStateChanged.Invoke(GameState.MainMenu);
                StartCoroutine(ShowMesage(true));
                
            }
        }
    }

    #region Login
    public void Login()
    {
        //loadingScreen.SetActive(true);

        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        string email = LoginEmail.text;
        string password = loginPassword.text;

        Credential credential =
        EmailAuthProvider.GetCredential(email, password);
        auth.SignInAndRetrieveDataWithCredentialAsync(credential).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAndRetrieveDataWithCredentialAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAndRetrieveDataWithCredentialAsync encountered an error: " + task.Exception);
                StartCoroutine(ShowMesage(false));
                return;
            }
            //loadingScreen.SetActive(false);
            AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
//My Added Line            
            GameplayEventsHUD.onGameStateChanged.Invoke(GameState.MainMenu);
            StartCoroutine(ShowMesage(true));
            

            if (result.User.IsEmailVerified)
            {
                //showLogMsg("Log in Successful");

                //loginUi.SetActive(false);
                //SuccessUi.SetActive(true);
                //SuccessUi.transform.Find("Desc").GetComponent<TextMeshProUGUI>().text = "Id: " + result.User.UserId;
            }
            else
            {
                //showLogMsg("Please verify email!!");
            }

        });




    }
    #endregion

    /*
    #region extra
    void showLogMsg(string msg)
    {
        logTxt.text = msg;
        logTxt.GetComponent<Animation>().Play("textFadeout");
    }
    #endregion
    */

    IEnumerator ShowMesage(bool success)
    {
        if (success)
        {
            SuccesMessage.SetActive(true);
        }
        else
        {
            FailureMessage.SetActive(true);
        }
        
        yield return new WaitForSeconds(1f);
        
        SuccesMessage.SetActive(false);
        FailureMessage.SetActive(false);
    }
}
