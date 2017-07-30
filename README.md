# We are Weeds
This is where anons can go after checking out /ggg/ - Go Green General, and wanting to learn more.

When it's done, you'll be able to talk with other anons, share seed combinations and bomb recipes, or even create your own recipes and combinations using an editor, plus some other dumb shit people like.

It'll be fancy as fuck.

The account system will not add identifiable information, so don't ask or submit a pull request adding that.

No email, none of that.

You don't have to give out info on 4chan, so you shouldn't have to give out info here.

The account system is just so faggots don't have to use tripcodes (projecting a bit here), and posting anonymously is the default option.

I will also not be blocking any VPN services, and encourage you to use one. You never know who's listening these days.


I decided to make it FOSS, so in the event I can no longer pay for it or work on it, someone can pick up the torch.

That, and so a basic layer of trust forms between us anons. I'm not out to get you, I just want to fix the ghettos.

## Setup

### Requirements
* MySQL Server
* Visual Studio 2017 + Microsoft .NET Core


### Setting up
* Clone the source
* In the FirstApp project, create an `appsettings.secret.json` file, with this as the contents:
~~~
{
    "ConnectionStrings": {
        "DefaultConnection": "host={0};database={1};userid={2};password={3};"
    },
    "Recaptcha": {
        "SiteKey": "Your Google ReCaptcha Site Key",
        "SecretKey": "Your Google ReCaptcha Secret Key"
    }
}
~~~

... filling in the required information as follows:
> {0} - The address for your database
> 
> {1} - The schema on that database for this project
> 
> {2} - A user you setup for the project
> 
> {3} - The password for that user

* Build the project
* Restore all migrations to your database

All good to go!

## TODO
* Finish thread system
* Finish account system
* Finish private messaging system
* Setup encryption for all data stored on the server
* Ban system (Region, IP, etc.) - Wonder how this will conflict w/ VPN use
* Better UI
* Add API endpoints (4chan has that, and it's pretty helpful)
* Spam prevention
* Better registration system (It's too easy to create accounts right now, that's what sucks about using no personal info)
* Delete Account functionality

## Common Questions

### Why did you do this?
* Discord is for faggots and NEETs (synonymous these days, no?)
* I wanted to tinker with .NET Core
* I already had a bunch of code for a social platform
* Things derail quickly in one thread on /pol/
* Bump limits suck
* Because I felt like it

### Why is the code so goddamn ugly?
It was originally an experimental project, where I put all my thoughts to code.

There's a lot of ugly things, but some of it is pretty neat. Feel free to rip it apart and make your own shit with it. I don't really care.

### How can I help?
I don't want you to be alarmed, but most of this is shit-tier software development. It could use some work.

If you feel inclined, you're welcome to submit a pull request. I reserve the right to reject it, but most of the time, if it's not blatantly obvious, I'll add a comment telling you why I rejected it.

If you want to contribute monetarily, please don't. I already have a job, I don't want your sympathy or pocket change. Fuck off.


### Who are you?
What does that matter? I'm just some cunt who saw a cool idea on /pol/.