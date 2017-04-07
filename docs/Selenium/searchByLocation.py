require "json"
require "selenium-webdriver"
require "rspec"
include RSpec::Expectations

describe "SearchByLocation" do

  before(:each) do
    @driver = Selenium::WebDriver.for :firefox
    @base_url = "https://latium.omm10.com/"
    @accept_next_alert = true
    @driver.manage.timeouts.implicit_wait = 30
    @verification_errors = []
  end
  
  after(:each) do
    @driver.quit
    @verification_errors.should == []
  end
  
  it "test_search_by_location" do
    @driver.get(@base_url + "/")
    @driver.find_element(:xpath, "//ul[2]/li[2]/a/strong").click
    @driver.find_element(:id, "Email").clear
    @driver.find_element(:id, "Email").send_keys "simplyvicky15@gmail.com"
    @driver.find_element(:id, "Password").clear
    @driver.find_element(:id, "Password").send_keys "abc123ABC123&"
    @driver.find_element(:id, "loginButton").click
    # Warning: verifyTextPresent may require manual changes
    verify { @driver.find_element(:css, "BODY").text.should =~ /^[\s\S]*$/ }
    @driver.find_element(:xpath, "//li[4]/a/strong").click
    @driver.find_element(:link, "Create New Asset").click
    @driver.find_element(:id, "name").clear
    @driver.find_element(:id, "name").send_keys "Country Tractor White Boat"
    Selenium::WebDriver::Support::Select.new(@driver.find_element(:id, "Makes")).select_by(:text, "FORD")
    Selenium::WebDriver::Support::Select.new(@driver.find_element(:id, "AssetCategories")).select_by(:text, "Truck")
    @driver.find_element(:id, "Address").clear
    @driver.find_element(:id, "Address").send_keys "here"
    Selenium::WebDriver::Support::Select.new(@driver.find_element(:id, "Cities")).select_by(:text, "Quebec")
    @driver.find_element(:id, "description").clear
    @driver.find_element(:id, "description").send_keys "here"
    @driver.find_element(:id, "price").clear
    @driver.find_element(:id, "price").send_keys "6000.00"
    @driver.find_element(:css, "input.btn.btn-primary").click
    Selenium::WebDriver::Support::Select.new(@driver.find_element(:id, "assetLocation")).select_by(:text, "Quebec")
    @driver.find_element(:css, "input[type=\"submit\"]").click
    @driver.find_element(:id, "userNavButton").click
    # Warning: verifyTextPresent may require manual changes
    verify { @driver.find_element(:css, "BODY").text.should =~ /^[\s\S]*$/ }
  end
  
  def element_present?(how, what)
    ${receiver}.find_element(how, what)
    true
  rescue Selenium::WebDriver::Error::NoSuchElementError
    false
  end
  
  def alert_present?()
    ${receiver}.switch_to.alert
    true
  rescue Selenium::WebDriver::Error::NoAlertPresentError
    false
  end
  
  def verify(&blk)
    yield
  rescue ExpectationNotMetError => ex
    @verification_errors << ex
  end
  
  def close_alert_and_get_its_text(how, what)
    alert = ${receiver}.switch_to().alert()
    alert_text = alert.text
    if (@accept_next_alert) then
      alert.accept()
    else
      alert.dismiss()
    end
    alert_text
  ensure
    @accept_next_alert = true
  end
end
