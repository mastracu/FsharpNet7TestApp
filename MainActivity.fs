namespace FsharpNet7TestApp

open System

open Android.App
open Android.Content
open Android.OS
open Android.Runtime
open Android.Views
open Android.Widget
open AndroidX.AppCompat.App
open Google.Android.Material.FloatingActionButton
open Google.Android.Material.Snackbar


type DoOnClick (action: View->Unit) =
    inherit Java.Lang.Object()
    interface View.IOnClickListener with
        member this.OnClick (view) = action view


[<Activity (Label = "FsharpNet7TestApp", Theme = "@style/AppTheme", MainLauncher = true, Icon = "@mipmap/icon")>]
type MainActivity () =
    // inherit Activity ()
    inherit AppCompatActivity ()

    let mutable count:int = 1

    override this.OnCreate (bundle) =
        base.OnCreate (bundle)

        // Set our view from the "main" layout resource
        this.SetContentView (Resource.Layout.Main)

        // Get our button from the layout resource, and attach an event to it
        let button = this.FindViewById<Button>(Resource.Id.myButton)
        let actionButton = this.FindViewById<FloatingActionButton>(Resource.Id.fab)

        let listener1 = new DoOnClick( fun view ->
            button.Text <- $"%d{count} clicks!"
            count <- count + 1
        )

        let listener2 = new DoOnClick( fun view ->
            Snackbar.Make(view, $"%d{count} clicks!", Snackbar.LengthLong).SetAction("Reset", new DoOnClick(fun view -> ())).Show()
            button.Text <- $"%d{count} clicks!"
            count <- count + 1
        )

        actionButton.SetOnClickListener (listener1)
        actionButton.SetOnClickListener (listener2)

        


