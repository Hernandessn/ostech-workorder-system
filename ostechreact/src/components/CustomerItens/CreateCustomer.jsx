import { Modal } from '../Modal';

const inputClass = (hasError) =>
    `rounded-md bg-[#021526] border px-3 py-2 text-[#E2E2B6] focus:outline-none ${
        hasError ? 'border-red-500 focus:border-red-500' : 'border-[#6EACDA]/40 focus:border-[#6EACDA]'
    }`;

const ErrorText = ({ message }) =>
    message ? <span className="text-red-500 text-sm">{message}</span> : null;

export const CreateCustomer = ({
    customer,
    isOpen,
    onClose,
    onChange,
    isSubmitting,
    onSubmit,
    errors
}) => {
    return (
        <Modal isOpen={isOpen} onClose={onClose} title="Create customer">
            <div className="flex flex-col gap-3">
                <label htmlFor="cat-name" className="text-sm font-medium">Name:</label>
                <input
                    id="cat-name"
                    type="text"
                    name="name"
                    value={customer.name}
                    onChange={onChange}
                    autoFocus
                    className={inputClass(errors.name)}
                />
                <ErrorText message={errors.name} />

                <label htmlFor="cat-email" className="text-sm font-medium">Email:</label>
                <input
                    id="cat-email"
                    type="email"
                    name="email"
                    value={customer.email}
                    onChange={onChange}
                    className={inputClass(errors.email)}
                />
                <ErrorText message={errors.email} />

                <label htmlFor="cat-phone" className="text-sm font-medium">Phone:</label>
                <input
                    id="cat-phone"
                    type="text"
                    name="phone"
                    value={customer.phone}
                    onChange={onChange}
                    className={inputClass(errors.phone)}
                />
                <ErrorText message={errors.phone} />

                <label htmlFor="cat-document" className="text-sm font-medium">Document:</label>
                <input
                    id="cat-document"
                    type="text"
                    name="document"
                    value={customer.document}
                    onChange={onChange}
                    className={inputClass(errors.document)}
                />
                <ErrorText message={errors.document} />
            </div>

            <div className="flex justify-end gap-2 mt-4">
                <button
                    onClick={onSubmit}
                    disabled={isSubmitting}
                    className="px-4 py-2 rounded-md bg-[#03346E] text-[#E2E2B6] hover:bg-[#6EACDA] hover:text-[#021526] transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
                >
                    {isSubmitting ? 'Adding...' : 'Add'}
                </button>
                <button
                    onClick={onClose}
                    className="px-4 py-2 rounded-md bg-red-600 text-white hover:bg-red-700 transition-colors"
                >
                    Cancel
                </button>
            </div>
        </Modal>
    );
};