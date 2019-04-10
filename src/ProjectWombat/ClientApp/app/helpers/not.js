import { helper } from '@ember/component/helper';

export function not(params) {
  if(params.length !== 1) {
    console.warn('Invalid number or arguments passed to or helper. Defaulting to false.');
    return false;
  }

  return !params[0];
}

export default helper(not);
