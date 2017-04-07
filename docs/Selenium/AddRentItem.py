# -*- coding: utf-8 -*-
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.support.ui import Select
from selenium.common.exceptions import NoSuchElementException
from selenium.common.exceptions import NoAlertPresentException
import unittest, time, re

class AddRentItem(unittest.TestCase):
    def setUp(self):
        self.driver = webdriver.Firefox()
        self.driver.implicitly_wait(30)
        self.base_url = "https://latium.omm10.com/"
        self.verificationErrors = []
        self.accept_next_alert = True
    
    def test_add_rent_item(self):
        driver = self.driver
        driver.get(self.base_url + "/")
        driver.find_element_by_xpath("//ul[2]/li[2]/a/strong").click()
        driver.find_element_by_id("Email").clear()
        driver.find_element_by_id("Email").send_keys("test@test.com")
        driver.find_element_by_id("Password").clear()
        driver.find_element_by_id("Password").send_keys("abc123ABC123&")
        driver.find_element_by_id("loginButton").click()
        # Warning: verifyTextPresent may require manual changes
        try: self.assertRegexpMatches(driver.find_element_by_css_selector("BODY").text, r"^[\s\S]*$")
        except AssertionError as e: self.verificationErrors.append(str(e))
        driver.find_element_by_xpath("//li[4]/a/strong").click()
        driver.find_element_by_link_text("Create New Asset").click()
        driver.find_element_by_id("for-rent").click()
        driver.find_element_by_id("name").clear()
        driver.find_element_by_id("name").send_keys("Test-Asset-For-Rent")
        Select(driver.find_element_by_id("Makes")).select_by_visible_text("FORD")
        Select(driver.find_element_by_id("AssetCategories")).select_by_visible_text("Bus")
        driver.find_element_by_id("Address").clear()
        driver.find_element_by_id("Address").send_keys("Rent-Address")
        Select(driver.find_element_by_id("Cities")).select_by_visible_text("Edmonton")
        driver.find_element_by_id("description").clear()
        driver.find_element_by_id("description").send_keys("Test-Description")
        driver.find_element_by_id("priceDaily").clear()
        driver.find_element_by_id("priceDaily").send_keys("2.00")
        driver.find_element_by_id("priceWeekly").clear()
        driver.find_element_by_id("priceWeekly").send_keys("3.00")
        driver.find_element_by_id("priceMonthly").clear()
        driver.find_element_by_id("priceMonthly").send_keys("4.00")
        driver.find_element_by_css_selector("input.btn.btn-primary").click()
        # Warning: verifyTextPresent may require manual changes
        try: self.assertRegexpMatches(driver.find_element_by_css_selector("BODY").text, r"^[\s\S]*$")
        except AssertionError as e: self.verificationErrors.append(str(e))
        driver.find_element_by_id("userNavButton").click()
        driver.find_element_by_link_text("Log Off").click()
    
    def is_element_present(self, how, what):
        try: self.driver.find_element(by=how, value=what)
        except NoSuchElementException as e: return False
        return True
    
    def is_alert_present(self):
        try: self.driver.switch_to_alert()
        except NoAlertPresentException as e: return False
        return True
    
    def close_alert_and_get_its_text(self):
        try:
            alert = self.driver.switch_to_alert()
            alert_text = alert.text
            if self.accept_next_alert:
                alert.accept()
            else:
                alert.dismiss()
            return alert_text
        finally: self.accept_next_alert = True
    
    def tearDown(self):
        self.driver.quit()
        self.assertEqual([], self.verificationErrors)

if __name__ == "__main__":
    unittest.main()
