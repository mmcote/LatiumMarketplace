# -*- coding: utf-8 -*-
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.support.ui import Select
from selenium.common.exceptions import NoSuchElementException
from selenium.common.exceptions import NoAlertPresentException
import unittest, time, re

class AddRequest(unittest.TestCase):
    def setUp(self):
        self.driver = webdriver.Firefox()
        self.driver.implicitly_wait(30)
        self.base_url = "https://localhost:44376/"
        self.verificationErrors = []
        self.accept_next_alert = True
    
    def test_add_request(self):
        driver = self.driver
        driver.get(self.base_url + "/")
        driver.find_element_by_link_text("Log in").click()
        driver.find_element_by_id("Email").clear()
        driver.find_element_by_id("Email").send_keys("test@test.com")
        driver.find_element_by_id("Password").clear()
        driver.find_element_by_id("Password").send_keys("abc123ABC123&")
        driver.find_element_by_id("loginButton").click()
        driver.find_element_by_link_text("Marketplace").click()
        driver.find_element_by_link_text("Create New Request").click()
        driver.find_element_by_id("name").clear()
        driver.find_element_by_id("name").send_keys("I need a big rig")
        Select(driver.find_element_by_id("Makes")).select_by_visible_text("BMW")
        Select(driver.find_element_by_id("AssetCategories")).select_by_visible_text("Car")
        driver.find_element_by_id("Address").clear()
        driver.find_element_by_id("Address").send_keys("Ardmore")
        Select(driver.find_element_by_id("Cities")).select_by_visible_text("Edmonton")
        driver.find_element_by_id("description").clear()
        driver.find_element_by_id("description").send_keys("Bigger the rig, better the rig")
        driver.find_element_by_id("accessory").clear()
        driver.find_element_by_id("accessory").send_keys("none")
        driver.find_element_by_css_selector("input.btn.btn-default").click()
        # Warning: verifyTextPresent may require manual changes
        try: self.assertRegexpMatches(driver.find_element_by_css_selector("BODY").text, r"^[\s\S]*$")
        except AssertionError as e: self.verificationErrors.append(str(e))
        driver.find_element_by_xpath("(//button[@type='button'])[2]").click()
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
