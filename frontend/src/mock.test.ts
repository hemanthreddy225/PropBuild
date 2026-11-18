import { describe, expect, it } from 'vitest';
import { mockTenants } from './mock';

describe('temporary tenant fixture', () => {
  it('contains tenant records with unique IDs and positive rent', () => {
    expect(new Set(mockTenants.map((tenant) => tenant.id)).size).toBe(mockTenants.length);
    expect(mockTenants.every((tenant) => tenant.rent > 0)).toBe(true);
  });
});
