import { Modal } from '../Modal';

const inputClass = (hasError) =>
    `rounded-md bg-[#021526] border px-3 py-2 text-[#E2E2B6] focus:outline-none ${
        hasError ? 'border-red-500 focus:border-red-500' : 'border-[#6EACDA]/40 focus:border-[#6EACDA]'
    }`;

const ErrorText = ({ message }) =>
    message ? <span className="text-red-500 text-sm">{message}</span> : null;

export const CreateTechnician = ({
    technician,
    isOpen,
    onClose,
    onChange,
    isSubmitting,
    onSubmit,
    errors
}) => {
    return (
        <Modal isOpen={isOpen} onClose={onClose} title="Create technician">
            <div className="flex flex-col gap-3">
                <label htmlFor="cat-name" className="text-sm font-medium">Name:</label>
                <input
                    id="cat-name"
                    type="text"
                    name="name"
                    value={technician.name}
                    onChange={onChange}
                    autoFocus
                    className={inputClass(errors.name)}
                />
                <ErrorText message={errors.name} />

                <label htmlFor="cat-specialty" className="text-sm font-medium">Specialty:</label>
                <input
                    id="cat-specialty"
                    type="text"
                    name="specialty"
                    value={technician.specialty}
                    onChange={onChange}
                    className={inputClass(errors.specialty)}
                />
                <ErrorText message={errors.specialty} />

                <label htmlFor="cat-contact" className="text-sm font-medium">Contact:</label>
                <input
                    id="cat-contact"
                    type="text"
                    name="contact"
                    value={technician.contact}
                    onChange={onChange}
                    className={inputClass(errors.contact)}
                />
                <ErrorText message={errors.contact} />

                <label htmlFor="cat-availability" className="text-sm font-medium">Availability:</label>
                <select
                    id="cat-availability"
                    name="availability"
                    value={technician.availability ? 'true' : 'false'}
                    onChange={(e) => onChange({
                        target: { name: 'availability', value: e.target.value }
                    })}
                    className={inputClass(errors.availability)}
                >
                    <option value="true">Available</option>
                    <option value="false">Unavailable</option>
                </select>
                <ErrorText message={errors.availability} />

                <label htmlFor="cat-hiringDate" className="text-sm font-medium">Hiring Date:</label>
                <input
                    id="cat-hiringDate"
                    type="date"
                    name="hiringDate"
                    value={technician.hiringDate}
                    onChange={onChange}
                    className={inputClass(errors.hiringDate)}
                />
                <ErrorText message={errors.hiringDate} />
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