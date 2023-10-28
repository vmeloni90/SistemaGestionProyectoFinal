/*
Template Name: GoodlyFit -  Admin & Dashboard Template
Author: TSGMLabs
Contact: TSGMLabs.in@gmail.com
File: Session Timeout Js File
*/

$.sessionTimeout({
	keepAliveUrl: '/PagesStarter',
	logoutButton:'Logout',
	logoutUrl: '/AuthLogin',
	redirUrl: '/AuthLockScreen',
	warnAfter: 3000,
	redirAfter: 30000,
	countdownMessage: 'Redirecting in {timer} seconds.'
});