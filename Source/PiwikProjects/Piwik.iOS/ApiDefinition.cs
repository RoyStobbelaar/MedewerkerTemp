using System;
using System.Drawing;

using ObjCRuntime;
using Foundation;
using UIKit;
using CoreData;
using CoreLocation;

namespace Org.Piwik.Sdk {

	// @interface PTEventEntity : NSManagedObject
	[BaseType (typeof (NSManagedObject))]
	interface PTEventEntity {

		// @property (retain, nonatomic) NSDate * date;
		[Export ("date", ArgumentSemantic.Retain)]
		NSDate Date { get; set; }

		// @property (retain, nonatomic) NSData * requestParameters;
		[Export ("requestParameters", ArgumentSemantic.Retain)]
		NSData RequestParameters { get; set; }
	}

	// @protocol PiwikDispatcher <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PiwikDispatcher {

		// @required -(void)sendSingleEventWithParameters:(NSDictionary *)parameters success:(void (^)())successBlock failure:(void (^)(BOOL))failureBlock;
		[Export ("sendSingleEventWithParameters:success:failure:")]
		[Abstract]
		void Success (NSDictionary parameters, Action successBlock, Action<sbyte> failureBlock);

		// @required -(void)sendBulkEventWithParameters:(NSDictionary *)parameters success:(void (^)())successBlock failure:(void (^)(BOOL))failureBlock;
		[Export ("sendBulkEventWithParameters:success:failure:")]
		[Abstract]
		void SuccessBulk (NSDictionary parameters, Action successBlock, Action<sbyte> failureBlock);
	}

	// @interface PiwikDebugDispatcher : NSObject <PiwikDispatcher>
	[BaseType (typeof (NSObject))]
	interface PiwikDebugDispatcher : PiwikDispatcher {

		// @property (nonatomic, strong) id<PiwikDispatcher> wrappedDispatcher;
		[Export ("wrappedDispatcher", ArgumentSemantic.Retain)]
		PiwikDispatcher WrappedDispatcher { get; set; }
	}

	// @interface PiwikLocationManager : NSObject
	[BaseType (typeof (NSObject))]
	interface PiwikLocationManager {

		// @property (readonly, nonatomic) CLLocation * location;
		[Export ("location")]
		CLLocation Location { get; }

		// -(void)startMonitoringLocationChanges;
		[Export ("startMonitoringLocationChanges")]
		void StartMonitoringLocationChanges ();

		// -(void)stopMonitoringLocationChanges;
		[Export ("stopMonitoringLocationChanges")]
		void StopMonitoringLocationChanges ();
	}

	// @interface PiwikNSURLSessionDispatcher : NSObject <PiwikDispatcher>
	[BaseType (typeof (NSObject))]
	interface PiwikNSURLSessionDispatcher : PiwikDispatcher {

		// -(instancetype)initWithPiwikURL:(NSURL *)piwikURL;
		[Export ("initWithPiwikURL:")]
		IntPtr Constructor (NSUrl piwikURL);
	}

	// @interface PiwikTrackedViewController : UIViewController
	[BaseType (typeof (UIViewController))]
	interface PiwikTrackedViewController {

		// @property (nonatomic, strong) NSString * trackedViewName;
		[Export ("trackedViewName", ArgumentSemantic.Retain)]
		string TrackedViewName { get; set; }
	}

	// @interface PiwikTracker : NSObject
	[BaseType (typeof (NSObject))]
	interface PiwikTracker {

		// @property (readonly, nonatomic) NSString * siteID;
		[Export ("siteID")]
		string SiteID { get; }

		// @property (readonly) id<PiwikDispatcher> dispatcher;
		[Export ("dispatcher")]
		PiwikDispatcher Dispatcher { get; }

		// @property (nonatomic, strong) NSString * userID;
		[Export ("userID", ArgumentSemantic.Retain)]
		string UserID { get; set; }

		// @property (nonatomic) BOOL isPrefixingEnabled;
		[Export ("isPrefixingEnabled")]
		bool IsPrefixingEnabled { get; set; }

		// @property (nonatomic) BOOL debug;
		[Export ("debug")]
		bool Debug { get; set; }

		// @property (nonatomic) BOOL optOut;
		[Export ("optOut")]
		bool OptOut { get; set; }

		// @property (nonatomic) NSUInteger sampleRate;
		[Export ("sampleRate")]
		nuint SampleRate { get; set; }

		// @property (nonatomic) BOOL includeDefaultCustomVariable;
		[Export ("includeDefaultCustomVariable")]
		bool IncludeDefaultCustomVariable { get; set; }

		// @property (nonatomic) BOOL sessionStart;
		[Export ("sessionStart")]
		bool SessionStart { get; set; }

		// @property (nonatomic) NSTimeInterval sessionTimeout;
		[Export ("sessionTimeout")]
		double SessionTimeout { get; set; }

		// @property (nonatomic) NSTimeInterval dispatchInterval;
		[Export ("dispatchInterval")]
		double DispatchInterval { get; set; }

		// @property (nonatomic) NSUInteger maxNumberOfQueuedEvents;
		[Export ("maxNumberOfQueuedEvents")]
		nuint MaxNumberOfQueuedEvents { get; set; }

		// @property (nonatomic) NSUInteger eventsPerRequest;
		[Export ("eventsPerRequest")]
		nuint EventsPerRequest { get; set; }

		// @property (nonatomic, strong) NSString * appName;
		[Export ("appName", ArgumentSemantic.Retain)]
		string AppName { get; set; }

		// @property (nonatomic, strong) NSString * appVersion;
		[Export ("appVersion", ArgumentSemantic.Retain)]
		string AppVersion { get; set; }

		// +(instancetype)sharedInstanceWithSiteID:(NSString *)siteID baseURL:(NSURL *)baseURL;
		[Static, Export ("sharedInstanceWithSiteID:baseURL:")]
		PiwikTracker SharedInstanceWithSiteID (string siteID, NSUrl baseURL);

		// +(instancetype)sharedInstanceWithSiteID:(NSString *)siteID dispatcher:(id<PiwikDispatcher>)dispatcher;
		[Static, Export ("sharedInstanceWithSiteID:dispatcher:")]
		PiwikTracker SharedInstanceWithSiteID (string siteID, PiwikDispatcher dispatcher);

		// +(instancetype)sharedInstance;
		[Static, Export ("sharedInstance")]
		PiwikTracker SharedInstance ();

		// -(BOOL)sendView:(NSString *)screen;
		[Export ("sendView:")]
		bool SendView (string screen);

		// -(BOOL)sendViews:(NSString *)screen, ...;
		[Export ("sendViews:")]
		bool SendViews (string screen);

		// -(BOOL)sendOutlink:(NSString *)url;
		[Export ("sendOutlink:")]
		bool SendOutlink (string url);

		// -(BOOL)sendEventWithCategory:(NSString *)category action:(NSString *)action name:(NSString *)name value:(NSNumber *)value;
		[Export ("sendEventWithCategory:action:name:value:")]
		bool SendEventWithCategory (string category, string action, [NullAllowed]string name, [NullAllowed]NSNumber value);

		// -(BOOL)sendExceptionWithDescription:(NSString *)description isFatal:(BOOL)isFatal;
		[Export ("sendExceptionWithDescription:isFatal:")]
		bool SendExceptionWithDescription (string description, bool isFatal);

		// -(BOOL)sendSocialInteraction:(NSString *)action target:(NSString *)target forNetwork:(NSString *)network;
		[Export ("sendSocialInteraction:target:forNetwork:")]
		bool SendSocialInteraction (string action, string target, string network);

		// -(BOOL)sendGoalWithID:(NSUInteger)goalID revenue:(NSUInteger)revenue;
		[Export ("sendGoalWithID:revenue:")]
		bool SendGoalWithID (nuint goalID, nuint revenue);

		// -(BOOL)sendSearchWithKeyword:(NSString *)keyword category:(NSString *)category numberOfHits:(NSNumber *)numberOfHits;
		[Export ("sendSearchWithKeyword:category:numberOfHits:")]
		bool SendSearchWithKeyword (string keyword, string category, NSNumber numberOfHits);

		// -(BOOL)sendTransaction:(PiwikTransaction *)transaction;
		[Export ("sendTransaction:")]
		bool SendTransaction (PiwikTransaction transaction);

		// -(BOOL)sendCampaign:(NSString *)campaignURLString;
		[Export ("sendCampaign:")]
		bool SendCampaign (string campaignURLString);

		// -(BOOL)sendContentImpressionWithName:(NSString *)name piece:(NSString *)piece target:(NSString *)target;
		[Export ("sendContentImpressionWithName:piece:target:")]
		bool SendContentImpressionWithName (string name, string piece, string target);

		// -(BOOL)sendContentInteractionWithName:(NSString *)name piece:(NSString *)piece target:(NSString *)target;
		[Export ("sendContentInteractionWithName:piece:target:")]
		bool SendContentInteractionWithName (string name, string piece, string target);

		// -(BOOL)setCustomVariableForIndex:(NSUInteger)index name:(NSString *)name value:(NSString *)value scope:(CustomVariableScope)scope;
		[Export ("setCustomVariableForIndex:name:value:scope:")]
		bool SetCustomVariableForIndex (nuint index, string name, string value, CustomVariableScope scope);

		// -(BOOL)dispatch;
		[Export ("dispatch")]
		bool Dispatch ();

		// -(void)deleteQueuedEvents;
		[Export ("deleteQueuedEvents")]
		void DeleteQueuedEvents ();
	}

	// @interface PiwikTransaction : NSObject
	[BaseType (typeof (NSObject))]
	interface PiwikTransaction {

		// -(id)initWithBuilder:(PiwikTransactionBuilder *)builder;
		[Export ("initWithBuilder:")]
		IntPtr Constructor (PiwikTransactionBuilder builder);

		// @property (readonly, nonatomic) NSString * identifier;
		[Export ("identifier")]
		string Identifier { get; }

		// @property (readonly, nonatomic) NSNumber * grandTotal;
		[Export ("grandTotal")]
		NSNumber GrandTotal { get; }

		// @property (readonly, nonatomic) NSNumber * subTotal;
		[Export ("subTotal")]
		NSNumber SubTotal { get; }

		// @property (readonly, nonatomic) NSNumber * tax;
		[Export ("tax")]
		NSNumber Tax { get; }

		// @property (readonly, nonatomic) NSNumber * shippingCost;
		[Export ("shippingCost")]
		NSNumber ShippingCost { get; }

		// @property (readonly, nonatomic) NSNumber * discount;
		[Export ("discount")]
		NSNumber Discount { get; }

		// @property (readonly, nonatomic) NSArray * items;
		[Export ("items")]
		NSObject [] Items { get; }

		// +(instancetype)transactionWithBuilder:(TransactionBuilderBlock)block;
		[Static, Export ("transactionWithBuilder:")]
		PiwikTransaction TransactionWithBuilder (Action<PiwikTransactionBuilder> block);
	}

	// @interface PiwikTransactionBuilder : NSObject
	[BaseType (typeof (NSObject))]
	interface PiwikTransactionBuilder {

		// @property (nonatomic, strong) NSString * identifier;
		[Export ("identifier", ArgumentSemantic.Retain)]
		string Identifier { get; set; }

		// @property (nonatomic, strong) NSNumber * grandTotal;
		[Export ("grandTotal", ArgumentSemantic.Retain)]
		NSNumber GrandTotal { get; set; }

		// @property (nonatomic, strong) NSNumber * subTotal;
		[Export ("subTotal", ArgumentSemantic.Retain)]
		NSNumber SubTotal { get; set; }

		// @property (nonatomic, strong) NSNumber * tax;
		[Export ("tax", ArgumentSemantic.Retain)]
		NSNumber Tax { get; set; }

		// @property (nonatomic, strong) NSNumber * shippingCost;
		[Export ("shippingCost", ArgumentSemantic.Retain)]
		NSNumber ShippingCost { get; set; }

		// @property (nonatomic, strong) NSNumber * discount;
		[Export ("discount", ArgumentSemantic.Retain)]
		NSNumber Discount { get; set; }

		// @property (nonatomic, strong) NSMutableArray * items;
		[Export ("items", ArgumentSemantic.Retain)]
		NSMutableArray Items { get; set; }

		// -(void)addItemWithSku:(NSString *)sku name:(NSString *)name category:(NSString *)category price:(float)price quantity:(NSUInteger)quantity;
		[Export ("addItemWithSku:name:category:price:quantity:")]
		void AddItemWithSku (string sku, string name, string category, float price, nuint quantity);

		// -(void)addItemWithSku:(NSString *)sku;
		[Export ("addItemWithSku:")]
		void AddItemWithSku (string sku);

		// -(PiwikTransaction *)build;
		[Export ("build")]
		PiwikTransaction Build ();
	}

	// @interface PiwikTransactionItem : NSObject
	[BaseType (typeof (NSObject))]
	interface PiwikTransactionItem {

		// @property (readonly, nonatomic) NSString * sku;
		[Export ("sku")]
		string Sku { get; }

		// @property (readonly, nonatomic) NSString * name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly, nonatomic) NSString * category;
		[Export ("category")]
		string Category { get; }

		// @property (readonly, nonatomic) NSNumber * price;
		[Export ("price")]
		NSNumber Price { get; }

		// @property (readonly, nonatomic) NSNumber * quantity;
		[Export ("quantity")]
		NSNumber Quantity { get; }

		// +(instancetype)itemWithSku:(NSString *)sku name:(NSString *)name category:(NSString *)category price:(float)price quantity:(NSUInteger)quantity;
		[Static, Export ("itemWithSku:name:category:price:quantity:")]
		PiwikTransactionItem ItemWithSku (string sku, string name, string category, float price, nuint quantity);

		// +(instancetype)itemWithSKU:(NSString *)sku;
		[Static, Export ("itemWithSKU:")]
		PiwikTransactionItem ItemWithSKU (string sku);

		// -(BOOL)isValid;
		[Export ("isValid")]
		bool IsValid ();
	}
}
