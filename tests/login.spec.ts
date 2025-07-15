import { test, expect } from '@playwright/test';

test('test', async ({ page }) => {
  await page.goto('http://localhost:4200/home');
  await (await page.getByRole('link', { name: 'Login' })).click();
  await page.waitForTimeout(500);

  await (await page.getByRole('textbox', { name: 'Username' })).click();
  await page.waitForTimeout(1500);

  await (await page.getByRole('textbox', { name: 'Username' })).fill('string');
  await page.waitForTimeout(1500);

  await (await page.getByRole('textbox', { name: 'Password' })).click();
  await page.waitForTimeout(1500);

  await (await page.getByRole('textbox', { name: 'Password' })).fill('string');
  await page.waitForTimeout(1500);

  page.once('dialog', async dialog => {
    console.log(`Dialog message: ${dialog.message()}`);
    await dialog.dismiss().catch(() => {});
  });
  await (await page.getByRole('button', { name: 'Login' })).click();
  await page.waitForTimeout(1500);

  await (await page.getByRole('button', { name: '👤 string (Doctor)' })).click();
  await page.waitForTimeout(1500);

  await (await page.getByRole('link', { name: 'Trang cá nhân' })).click();
  await page.waitForTimeout(1500);

  await (await page.getByRole('button', { name: '👤 string (Doctor)' })).click();
  await page.waitForTimeout(1500);

  await (await page.getByRole('button', { name: 'Đăng xuất' })).click();
  await page.waitForTimeout(1500);
});