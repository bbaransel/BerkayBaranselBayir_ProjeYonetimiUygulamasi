using Yonetimsell.Shared.Helpers.Abstract;

namespace Yonetimsell.Shared.Helpers.Concrete
{
    public class SweetAlertManager: ISweetAlertService
    {
        public string TopEndNotification(string icon, string title)
        {
            return $@"<script>
                        const Toast = Swal.mixin({{
                            toast: true,
                            position: 'top-end',
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,
                            didOpen: (toast) => {{
                            toast.onmouseenter = Swal.stopTimer;
                            toast.onmouseleave = Swal.resumeTimer;
                            }}
                        }});
                        Toast.fire({{
                            icon: '{icon}',
                            title: '{title}'
                        }});
                    </script>";
        }
    }
}
